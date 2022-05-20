using GitHubClient.Api.Models;
using MediatR;

namespace GitHubClient.Api.Query
{
    public class GetAllRepositoriesQuery: IRequest<List<Repository>>
    {
        public string Owner { get; set; }
        public string Repo { get; set; }

        public GetAllRepositoriesQuery(string owner, string repo)
        {
            Owner = owner;
            Repo = repo;
        }
    }
}
