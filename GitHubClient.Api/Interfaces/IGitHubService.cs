using GitHubClient.Api.Dtos;
using GitHubClient.Api.Models;

namespace GitHubClient.Api.Interfaces
{
    public interface IGitHubService
    {       
        public Task<List<string>> GetAllAuthors(string owner, string repo, FilterDto filter);
    }
}
