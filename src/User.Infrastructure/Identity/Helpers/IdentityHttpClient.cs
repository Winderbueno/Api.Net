using User.Infrastructure.Identity.Dtos;
using Newtonsoft.Json;
using System.Text;


namespace User.Infrastructure.Identity.Helpers
{
    public static class IdentityHttpClientExtensions
    {
        public static async Task<ResultResponse<TOut>?> PostAsyncIdentity<TIn, TOut>(this HttpClient client, string route, TIn payload) where TOut : class
        {
            var response = await client.PostAsync(
                route,
                new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

            return DeserializeIfSuccessAsync<TOut>(response).Result;
        }

        public static async Task<ResultResponse<TOut>?> GetAsyncIdentity<TOut>(this HttpClient client, string route) where TOut : class
        {
            var response = await client.GetAsync(route);

            return DeserializeIfSuccessAsync<TOut>(response).Result;
        }

        static async Task<ResultResponse<T>?> DeserializeIfSuccessAsync<T>(HttpResponseMessage response) where T : class
        {
            // bid.api BadRequest response can contain ResultResponse meaning it is a catched exception in bid.api
            //  => In this case, no error should be raised
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var res = JsonConvert.DeserializeObject<ResultResponse<T>>(await response.Content.ReadAsStringAsync());

                if (res == null)
                    throw new ApplicationException("An error occured while fetching the information in bid.api");

                return res;
            }

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<ResultResponse<T>>(await response.Content.ReadAsStringAsync());
        }
    }
}
