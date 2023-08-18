using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Application.Services.Interfaces;

namespace Api.Middlewares.Authentication;

public class ClaimsTransformation : IClaimsTransformation
{
  private readonly IUserService _userService;

  public ClaimsTransformation(IUserService userService)
    => _userService = userService;

  public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
  {
    var clone = principal.Clone();

    /*
    // Get IdentityId in principal claims
    var identityId = principal.Claims.ToList().Find(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    // Get related user
    var user = await _userService.GetAsync(1); // Todo

    // Transform permissions in Role claims
    if (principal.Identity!.IsAuthenticated && user != null)
    {
        user.Permissions.ToList().ForEach(p => {
            ((ClaimsIdentity)clone.Identity)!.AddClaim(
                new Claim(ClaimTypes.Role, p));
        });
    }*/

    return clone;
  }
}