using GitHubClient.Api.Interfaces;
using GitHubClient.Api.Models;
using MediatR;

namespace GitHubClient.Api.Query
{
    public class GitHubQueryHandler:
         IRequestHandler<GetAllAuthorsQuery, List<string>>
        //,         IRequestHandler<GetAllRepositoriesQuery, List<Repository>>
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
        //public Task<List<Repository>> Handle(GetAllRepositoriesQuery request, CancellationToken cancellationToken)
        //{
        //    return _service.GetAllRepositories(request.Owner, request.Repo, );
        //}
    }
}
