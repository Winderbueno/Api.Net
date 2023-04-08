using User.Infrastructure.Identity.Dtos;

namespace User.Infrastructure.Identity.Helpers;

public static class IdentityHttpClientStubExtensions
{
  public static ResultResponse<IdentityDto> GetAsyncIdentityStub(this HttpClient client)
  {
    ResultResponse<IdentityDto> resp = new () { 
      Succeeded =true,
      Result = new IdentityDto {
        FirstName = "Mister",
        LastName = "K"
      } 
    };

    return resp;
  }
}