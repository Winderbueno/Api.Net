using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using User.Application.Dtos;
using User.Application.Exceptions;
using User.Application.Services.Interfaces;
using User.Domain.Entities;
using User.Infrastructure.Identity.Dto;
using User.Infrastructure.Identity.Helpers;
using User.Persistence.Db;
using User.Persistence.Repositories.Interfaces;

namespace User.Application.Services
{
    public  class UserService : IUserService
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
            UserDbContext userDb)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _identityApi = new HttpClient();
            _userDb = userDb;
        }


        public async Task<IEnumerable<int>> GetAsync() 
          => (await _userRepo.Get()).ToList();                

        public async Task<UserDto> GetAsync(int id)
        {
            // Get user (.db)
            var user = await _userRepo.Get(id, true);
            if(user == null) throw new NotFoundException("User", "id", id.ToString());

            // Get Identity
            var resp = _identityApi.GetAsyncIdentityStub();
            if(resp?.Succeeded == false) throw new ApplicationException("identity.api call error");

            // Build response
            return (mapUserDto(user, resp!.Result!))!;
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            // Input check
            var role = await _roleRepo.Get(userDto.RoleId, true);
            if (role == null) throw new ValidationException($"Role with id : {userDto.RoleId}, not found in db");

            // Create/Updt Identity
            // Todo - Call Api + Handle Errors (Identity already exists...)
            IdentityDto identityDto = _mapper.Map<IdentityDto>(userDto);
            var resp = await _identityApi.PostAsyncIdentity<IdentityDto[], IdentityDto[]>("/users", new[] { identityDto });
            if (resp?.Succeeded == false)
            {
                _logger.LogError("Could not create identity: {@errors}", resp?.Errors);
            }

            // Create user (.db)
            var identityId = Guid.Empty;
            var user = await _userRepo.GetByIdentityId(identityId);
            if(true){
                user = new UserK { IdentityId = identityId, RoleId = userDto.RoleId }; 
                await _userDb.AddAsync(userDto);
                _userDb.SaveChanges();
            }
            
            // Get user permissions (.db)
            user = await _userRepo.Get(user.UserId, true);

            // Build response
            return mapUserDto(user!, resp!.Result!.First());
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            // Input check
            var user = await _userRepo.Get(userDto.UserId, true);
            if (user == null) throw new NotFoundException("User", "id", userDto.UserId.ToString());
            var role = await _roleRepo.Get(userDto.RoleId);
            if (role == null) throw new ValidationException($"Role with id : {userDto.RoleId}, not found in db");

            // Get Identity
            var resp = await _identityApi.GetAsyncIdentity<IdentityDto>($"/users/{user.IdentityId}");

            // Update user (.db)
            user!.RoleId = userDto.RoleId;
            _userDb.SaveChanges();

            // Build response
            return mapUserDto(user, resp.Result);
        }

        

        private UserDto mapUserDto(UserK user, IdentityDto identityDto)
        {
            // Map Identity
            UserDto userDto = _mapper.Map<UserDto>(identityDto);

            // Map User
            if (userDto != null)
            {
                userDto.UserId = user.UserId;
                userDto.RoleId = user.RoleId;
                userDto.Permissions = FlattenPermission(user);
            }

            return userDto;
        }

        private List<string> FlattenPermission(UserK user)
        {
            return user.Role?.Features?.ToList()
              .Aggregate(
                new List<string>(),
                (result, value) => {
                  result.AddRange(value.Permissions.Select(p => p.Name));
                  return result;
                }) ?? new List<string>();
        }
    }
}
