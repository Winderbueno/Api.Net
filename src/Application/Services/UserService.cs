﻿using AutoMapper;
using Application.Dtos;
using Application.Exceptions;
using Application.Services.Interfaces;
using Domain.Entities;
using User.Infrastructure.Identity.Constants;
using User.Infrastructure.Identity.Dto;
using User.Infrastructure.Identity.Helpers;
using Persistence.DbContexts;
using Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public  class UserService: IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly HttpClient _identityApi;
        private readonly UserDbContext _userDb;

        public UserService(
            ILogger<UserService> logger,
            IMapper mapper,
            IUserRepository userRepo,
            IRoleRepository roleRepo,
            IHttpClientFactory factory,
            UserDbContext userDb)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _identityApi = factory.CreateClient(HttpClientName.IdentityApi);
            _userDb = userDb;
        }


        public async Task<IEnumerable<int>> GetAsync() 
          => (await _userRepo.Get()).ToList();                

        public async Task<UserDto> GetAsync(int id)
        {
            // Get user (.db)
            var user = await _userRepo.Get(id, true);
            if(user == null) throw new EntityNotFoundException("User", "id", id.ToString());

            // Get Identity
            var resp = await _identityApi.GetAsyncIdentity<Identity>($"/identities/{user.IdentityId}");

            // Build response
            return mapUserDto(user, resp.Result);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            // Input check
            var role = await _roleRepo.Get(userDto.RoleId, true);
            if (role == null) throw new ValidationException($"Role with id : {userDto.RoleId}, not found in db");

            // Create/Updt Identity
            // Todo - Call Api + Handle Errors (Identity already exists...)
            Identity identity = _mapper.Map<Identity>(userDto);
            var resp = await _identityApi.PostAsyncIdentity<Identity[], Identity[]>("/users", new[] { identity });
            if (resp?.Succeeded == false)
            {
                _logger.LogError("Could not create identity: {@errors}", resp?.Errors);
            }

            // Create user (.db)
            var identityId = Guid.Empty;
            var user = await _userRepo.GetByIdentityId(identityId);
            if(true){
                user = new User { IdentityId = identityId, RoleId = userDto.RoleId }; 
                await _userDb.AddAsync(userDto);
                _userDb.SaveChanges();
            }
                         
            
            // Get user permissions (.db)
            user = await _userRepo.Get(user.UserId, true);

            // Build response
            return mapUserDto(user, resp.Result.First());
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            // Input check
            var user = await _userRepo.Get(userDto.UserId, true);
            if (user == null) throw new EntityNotFoundException("User", "id", userDto.UserId.ToString());
            var role = await _roleRepo.Get(userDto.RoleId);
            if (role == null) throw new ValidationException($"Role with id : {userDto.RoleId}, not found in db");

            // Get Identity
            //var resp = await _bidApi.GetAsyncBid<UserBid>($"/users/{user.IdentityId}");

            // Update user (.db)
            user!.RoleId = userDto.RoleId;
            _userDb.SaveChanges();

            // Build response
            return mapUserDto(user, resp.Result);
        }

        

        private UserDto mapUserDto(User user, Identity userBid)
        {
            // Map Baloise identity
            UserDto userDto = _mapper.Map<UserDto>(userBid);

            // Map  user
            if (userDto != null)
            {
                userDto.UserId = user.UserId;
                userDto.RoleId = user.RoleId;
                userDto.Permissions = mapPermissions(user);
            }

            return userDto;
        }

        private List<string> mapPermissions(User user)
        {
            List<string> permissions = new List<string>();
            List<Feature> features = user.Role?.Features?.ToList();
            features?.ForEach(f => { f.Permissions.ToList().ForEach(p => permissions.Add(p.Name)); });
            return permissions;
        }
    }
}
