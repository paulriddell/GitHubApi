using GitHubClient.Api.Dtos;
using GitHubClient.Api.Models;

namespace GitHubClient.Api.Interfaces
{
    public interface IGitHubService
    {       
        /// <summary>
        /// Gets authors from last 30 commits from github repo
        /// </summary>
        /// <param name="owner">owner</param>
        /// <param name="repo">repo</param>
        /// <param name="filter">optional parameters for number of authors and page number to return</param>
        /// <returns>List of author names from latest commits</returns>
        public Task<List<string>> GetAllAuthors(string owner, string repo, FilterDto filter);
    }
}
