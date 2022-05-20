using GitHubClient.Api.Query;
using Moq;
using GitHubClient.Api.Interfaces;
using GitHubClient.Api.Dtos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubApiClient.Tests.Query
{
    [TestClass]
    public class GetAllAuthorsQueryTests
    {

        private GitHubQueryHandler _queryHandler;
        private readonly Mock<IGitHubService> _mockGitHubService = new();
        private const string OWNER = "solana-labs";
        private const string REPO = "solana";

        [TestInitialize]
        public void Initialize()
        {
            var authors = new List<string>()
            {
                "john",
                "sam",
                "amy"
            };

            _mockGitHubService.Setup(s => s.GetAllAuthors(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<FilterDto>()))
                .ReturnsAsync(authors);

            _queryHandler = new GitHubQueryHandler(_mockGitHubService.Object);
        }

        [TestMethod]
        public async Task GivenInvalidOwner_GetAllAuthorsQuery_ThrowsException()
        {
            //Arrange
            _mockGitHubService.Setup(s => s.GetAllAuthors("invalid", It.IsAny<string>(), It.IsAny<FilterDto>()))
               .Throws(new NullReferenceException("Owner and/or repo are null"));

            // Act
            Func<Task> result = async () => await _queryHandler.Handle(new GetAllAuthorsQuery("invalid", REPO), new CancellationToken());

            // Assert 
            await result.Should().ThrowAsync<NullReferenceException>().WithMessage("Owner and/or repo are null");
        }

        [TestMethod]
        public async Task GetAllAuthorsQuery_ReturnAllAuthors()
        {
            // Act
            var result = await _queryHandler.Handle(new GetAllAuthorsQuery(OWNER, REPO), new CancellationToken());

            // Assert 
            result.Should().HaveCount(3);
        }
    }
}
