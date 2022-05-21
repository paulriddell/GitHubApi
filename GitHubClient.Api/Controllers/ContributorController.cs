
using Microsoft.AspNetCore.Mvc;
using MediatR;
using GitHubClient.Api.Query;
using GitHubClient.Api.Dtos;

namespace GitHubClient.Api.Controllers
{
    [ApiVersion("1")]    
    [ApiController]
    public class ContributorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContributorController> _logger;

        public ContributorController(ILogger<ContributorController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet, MapToApiVersion("1")]
        [Route("api/v{version:apiVersion}/{owner}/{repo}/contributors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<string>>> Get(string owner, string repo, [FromQuery] FilterDto optionalParameters)
        {
            _logger.LogInformation("Getting contributors from github");

            try
            {
                var query = new GetAllAuthorsQuery(owner, repo);

                if (!string.IsNullOrEmpty(optionalParameters.PageNumber) ||
                    !string.IsNullOrEmpty(optionalParameters.PageSize))
                {
                    query.Filter = optionalParameters;
                }

                return await _mediator.Send(query);
            }
            catch (BadHttpRequestException be)
            {
                return NotFound(be.Message);
            }
            catch (Exception ex)
            {
                // given more time i would improve error handling and report
                // out correct status codes more eligantly
                return BadRequest(ex.Message);
            }            
        }
    }
}
