using Infrastructure.Identity.Dtos;

namespace Infrastructure.Identity.Helpers;

public static class IdentityHttpClientStubExtensions
{
  public static ResultResponse<IdentityDto> GetIdentityAsyncStub(this HttpClient client)
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