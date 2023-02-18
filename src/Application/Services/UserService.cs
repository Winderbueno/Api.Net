using AutoMapper;
using Application.Dtos;
using Application.Exceptions;
using Application.Services.Interfaces;
using Domain.Entities;
// using Infrastructure.Bid.Constants;
// using Infrastructure.Bid.Dto;
// using Infrastructure.Bid.Helpers;
 using Persistence.DbContexts;
 using Persistence.Repositories.Interfaces;
// using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public  class UserService: IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly HttpClient _bidApi;
        private readonly ApiDbContext _apiDb;

        public UserService(
            ILogger<UserService> logger,
            IMapper mapper,
            IUserRepository userRepo,
            IRoleRepository roleRepo,
            //IHttpClientFactory factory,
            ApiDbContext apiDb)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            //_bidApi = factory.CreateClient(HttpClientName.BidApi);
            _apiDb = apiDb;
        }


        // Get users (.db)
        public async Task<List<int>> GetAsync() => _userRepo.GetUsers();                

        public async Task<UserDto> GetAsync(int id)
        {
            // Get user (.db)
            var user = _userRepo.GetUserById(id, true);
            if(user == null) throw new NotFoundException($"User with id : {id}, not found in db");

            // Get Bid
            //var resp = await _bidApi.GetAsyncBid<UserBid>($"/users/{user.IdentityId}");

            // Build response
            return mapUserDto(user, resp.Result);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            // Input check
            var role = _roleRepo.GetRoleById(userDto.RoleId, true);
            if (role == null) throw new ValidationException($"Role with id : {userDto.RoleId}, not found in db");

            // Create/Updt Bid
            // Todo - Call bid.api/ (=users/create/pour) [Non dispo now]
            // Todo - Gerer erreurs (Bid existe deja, ...) [Note. DtoCheck fait ds Controller]
            UserBid userBid = _mapper.Map<UserBid>(userDto);
            var resp = await _bidApi.PostAsyncBid<UserBid[], UserBid[]>("/users", new[] { userBid });
            if (resp?.Succeeded == false)
            {
                _logger.LogError("Could not create user in bid.db: {@errors}", resp?.Errors);
                //return new UserDto {
                //    IsSuccess = false,
                //    Errors = resp?.Errors?.SelectMany(e => e.Split(Environment.NewLine))
                //};
            }

            // Create user (.db)
            // Todo - Doit-on verifier qu'un user existe deja avec le meme TypeUser ?
            //      Oui, si on veut gérer un Client MyBaloise qui serait aussi Agent (avec la mm Identite)
            userBid.Id = Guid.Parse("370610a7-6736-4da0-9867-07a2478e9f21"); // Todo - only for test
            User user = _userRepo.AddUser(new User {
                IdentityId = userBid.Id,
                RoleId = userDto.RoleId
            });                
            
            // Get user permissions (.db)
            user = _userRepo.GetUserById(user.UserId, true);

            // Build response
            return mapUserDto(user, resp.Result.First());
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            // Input check
            var user = _userRepo.GetUserById(userDto.UserId, true);
            if (user == null) throw new NotFoundException($"User with id : {userDto.UserId}, not found in db");
            var role = _roleRepo.GetRoleById(userDto.RoleId);
            if (role == null) throw new ValidationException($"Role with id : {userDto.RoleId}, not found in db");

            // Update user (.db)
            user!.RoleId = userDto.RoleId;
            _Db.SaveChanges();

            // Get Bid
            //var resp = await _bidApi.GetAsyncBid<UserBid>($"/users/{user.IdentityId}");

            // Build response
            return mapUserDto(user, resp.Result);
        }

        

        private UserDto mapUserDto(User user, UserBid userBid)
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
