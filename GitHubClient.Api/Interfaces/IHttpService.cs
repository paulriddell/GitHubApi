namespace GitHubClient.Api.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> requestHeaders);
        
        //easily extended to handle put and post 
    }
}
