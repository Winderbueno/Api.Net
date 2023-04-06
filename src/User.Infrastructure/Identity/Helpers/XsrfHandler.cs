using Microsoft.Net.Http.Headers;

namespace User.Infrastructure.Identity.Helpers
{
    public class XsrfHandler : DelegatingHandler
    {   
        const string XsrfCookieName = "XSRF-TOKEN";
        const string XsrfHeaderName = "X-XSRF-TOKEN";

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            var refreshXsrfTokenMesssage = new HttpRequestMessage() {
                RequestUri = new Uri(request.RequestUri!, "/security/refreshxsrftoken"),
                Method = HttpMethod.Get,
            };

            // XSRF-token is filled with user infos, it needs to be authenticated to be correct
            refreshXsrfTokenMesssage.Headers.Authorization = request.Headers.Authorization;
            var response = await base.SendAsync(refreshXsrfTokenMesssage, cancellationToken);

            if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
            {
                request.Headers.Add("Cookie", cookies);

                var xsrfCookie = SetCookieHeaderValue.ParseList(cookies.ToList()).FirstOrDefault(c => c.Name == XsrfCookieName);
                if (xsrfCookie != null) request.Headers.Add(XsrfHeaderName, xsrfCookie.Value.ToString());
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
