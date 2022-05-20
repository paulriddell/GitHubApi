using GitHubClient.Api.Query;
using Moq;
using GitHubClient.Api.Interfaces;
using GitHubClient.Api.Dtos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubClient.Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace GitHubApiClient.Tests.Query
{
    [TestClass]
    public class GitHubServiceTests
    {
        private GitHubService _service;
        private readonly Mock<IHttpService> _mockHttpService = new();
        private readonly Mock<ILogger<GitHubService>> _mockLogger = new();
        private readonly Mock<IConfiguration> _mockConfiguration = new();
        private const string OWNER = "solana-labs";
        private const string REPO = "solana";
        private FilterDto _filter;

        [TestInitialize]
        public void Initialize()
        {
            StreamReader r = new StreamReader("githubrepos.json");
            string jsonString = r.ReadToEnd();

            var mockHttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonString)
            };

            _mockHttpService.Setup(s =>
                    s.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(mockHttpResponse);

            _mockConfiguration.Setup(c =>c["Constants:PageSize"]).Returns("30");
            _mockConfiguration.Setup(c => c["Constants:PageNumber"]).Returns("1");
            _mockConfiguration.Setup(c => c["Uri:GitHubRepo"]).Returns("https://api.github.com/repos");

            _service = new GitHubService(_mockLogger.Object, _mockHttpService.Object, _mockConfiguration.Object);
        }

        [TestMethod]
        public async Task GetAllAuthors_ReturnAllAuthors()
        {
            // Act
            var result = await _service.GetAllAuthors(OWNER, REPO, _filter);

            // Assert 
            result.Should().HaveCount(30);
        }

    }
}
