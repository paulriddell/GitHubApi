using GitHubClient.Api.Interfaces;

namespace GitHubClient.Api.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> requestHeaders)
        {
            var request = CreateRequest(HttpMethod.Get, url, null, requestHeaders);
            return await _httpClient.SendAsync(request);
        }

        public static HttpRequestMessage CreateRequest(HttpMethod method, string uri, HttpContent content, Dictionary<string, string> requestHeaders = null, Dictionary<string, string> contentHeaders = null)
        {
            //future proofed for other request types (put, post)

            var request = new HttpRequestMessage(method, uri);
            if (content != null)
                request.Content = content;

            if (requestHeaders == null)
                return request;

            foreach (var (key, value) in requestHeaders)
            {
                request.Headers.Add(key, value);
            }

            if (contentHeaders == null)
                return request;

            foreach (var (key, value) in contentHeaders)
            {
                request.Content.Headers.Add(key, value);
            }

            return request;
        }
    }
}
