using User.Infrastructure.Identity.Dto;

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