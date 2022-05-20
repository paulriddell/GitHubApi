using GitHubClient.Api.Dtos;
using GitHubClient.Api.Interfaces;
using GitHubClient.Api.Models;
using System.Text.Json;

namespace GitHubClient.Api.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly ILogger<GitHubService> _logger;
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public GitHubService(ILogger<GitHubService> logger,
                             IHttpService httpService,
                             IConfiguration configuration)
        {
            _logger = logger;
            _httpService = httpService;
            _configuration = configuration;
        }

        public async Task<List<Repository>> GetAllRepositories(string owner, string repo, FilterDto filter)
        {
            if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(repo))
            {
                throw new NullReferenceException("Owner and/or repo are null");
            }
            if (filter == null)
            {
                _logger.LogInformation($"{nameof(filter)} is null, no optional parameters found");
            }

            var pageSize = filter?.PageSize ?? _configuration["Constants:PageSize"];
            var pageNumber = filter?.PageNumber ??_configuration["Constants:PageNumber"];
            var baseUri = _configuration["Uri:GitHubRepo"];
            var uri = $"{baseUri}/{owner}/{repo}/commits?per_page={pageSize}&page={pageNumber}";

            var requestHeaders = new Dictionary<string, string>()
              {
                  { "Accept", "application/vnd.github.v3+json"},
                  { "User-Agent", "GitHubAPIClient"}
              };

            var response = await _httpService.GetAsync(uri, requestHeaders);
            if (!response.IsSuccessStatusCode)
            {
                throw new BadHttpRequestException("GitHub owner or repo not found.");
            }

            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(response.Content.ReadAsStream());

            if (repositories == null)
            {
                throw new NullReferenceException("Repositories not found after deserialization.");
            }

            return repositories;
        }

        public async Task<List<string>> GetAllAuthors(string owner, string repo, FilterDto filter)
        {
            if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(repo))
            {
                throw new NullReferenceException("Owner and/or repo are null");
            }
            if (filter == null)
            {
                _logger.LogInformation($"{nameof(filter)} is null, no optional parameters found");
            }

            var repos = await GetAllRepositories(owner, repo, filter);

            if (repos == null || !repos.Any())
            {
                throw new NullReferenceException("Repositories not found after deserialization");
            }

            return repos.Where(c=>c.Commit != null & c.Commit.Author != null).Select(c => c.Commit?.Author.Name).ToList();
        }
    }
}
