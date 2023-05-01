using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using User.Application.Dtos;
using User.Application.Mappers.Interfaces;
using User.Application.Services.Interfaces;
using User.Domain.Entities;
using User.Domain.Exceptions;
using User.Infrastructure.Identity.Dtos;
using User.Infrastructure.Identity.Helpers;
using User.Persistence.Db;
using User.Persistence.Repositories.Interfaces;

namespace User.Application.Services;

public class UserService : IUserService
{
  private readonly ILogger<UserService> _logger;
  private readonly IMapper _mapper;
  private readonly IUserMapper _userMapper;
  private readonly IUserRepository _userRepo;
  private readonly IRoleRepository _roleRepo;
  private readonly HttpClient _identityApi;
  private readonly UserDbContext _userDb;

  public UserService(
      ILogger<UserService> logger,
      IMapper mapper,
      IUserMapper userMapper,
      IUserRepository userRepo,
      IRoleRepository roleRepo,
      UserDbContext userDb)
  {
    _logger = logger;
    _mapper = mapper;
    _userMapper = userMapper;
    _userRepo = userRepo;
    _roleRepo = roleRepo;
    _identityApi = new HttpClient();
    _userDb = userDb;
  }

  public async Task<UserDto> GetAsync(int id)
  {
    // Get user (.db)
    var user = await _userRepo.GetAsync(id, true);
    if (user == null) throw new NotFoundException("User", "id", id.ToString());

    // Get Identity
    var resp = _identityApi.GetIdentityAsyncStub();
    if (resp?.Succeeded == false) throw new ApplicationException("identity.api call error");

    // Build response
    return _userMapper.ToUserDto(user, resp!.Result);
  }

  public async Task<IEnumerable<int>> SearchAsync(UserSearchDto? dto)
    {
        // Search bisa.db
        var users = await _userRepo.SearchAsync(dto?.Types, dto?.Functions);

        // Search identities
        // var resp = await _bidApi.PostAsyncBid<UserSearchBidDto, IEnumerable<int>>(
        //     "/identities/search", new UserSearchBidDto() { SearchString = dto?.Contains ?? "" });

        // Build response filtering with searched identities
        return users.Select(u => u!.UserKId);
    }

  public async Task<UserDto> CreateAsync(UserDto dto)
  {
    // Validation
    var role = await _roleRepo.GetAsync(dto.RoleId, true);
    if (role == null) throw new ValidationException(new[] { new ValidationFailure("role", "not found in db") });

    // Create/updt Identity
    // Todo - Call Api + Handle Errors (Identity already exists...)
    IdentityDto identityDto = _mapper.Map<IdentityDto>(dto);
    var resp = await _identityApi.PostAsyncIdentity<IdentityDto[], IdentityDto[]>("/users", new[] { identityDto });
    if (resp?.Succeeded == false)
    {
      _logger.LogError("Could not create identity: {@errors}", resp?.Errors);
      throw new ApplicationException(resp?.Errors!.First());
    }

    // Create user (.db)
    var identityId = Guid.Empty;
    var user = await _userRepo.GetByIdentityIdAsync(identityId);
    if (user == null)
    {
      user = new UserK { IdentityId = identityId, RoleId = dto.RoleId };
      await _userDb.AddAsync(dto);
      _userDb.SaveChanges();
    }
    else throw new ApplicationException("User already exists");

    // Get user permissions (.db)
    user = await _userRepo.GetAsync(user.UserKId, true);

    // Build response
    return _userMapper.ToUserDto(user!, resp!.Result.First());
  }

  public async Task<UserDto> UpdateAsync(UserDto dto)
  {
    // Validation
    var user = await _userRepo.GetAsync(dto.UserId, true);
    if (user == null) throw new NotFoundException("User", "id", dto.UserId.ToString());
    var role = await _roleRepo.GetAsync(dto.RoleId);
    if (role == null) throw new ValidationException(new[] { new ValidationFailure("role", "not found in db") });

    // Get Identity
    var resp = await _identityApi.GetAsyncIdentity<IdentityDto>($"/users/{user.IdentityId}");

    // Update user (.db)
    user!.RoleId = dto.RoleId;
    _userDb.SaveChanges();

    // Build response
    return _userMapper.ToUserDto(user, resp!.Result);
  }
}