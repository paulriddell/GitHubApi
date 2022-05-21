using GitHubClient.Api.Interfaces;
using MediatR;

namespace GitHubClient.Api.Query
{
    public class GitHubQueryHandler: IRequestHandler<GetAllAuthorsQuery, List<string>>
    {
        private readonly IGitHubService _service;

        public GitHubQueryHandler(IGitHubService service)
        {
            _service = service;
        }

        public Task<List<string>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return _service.GetAllAuthors(request.Owner, request.Repo, request.Filter);
        }
    }
}
