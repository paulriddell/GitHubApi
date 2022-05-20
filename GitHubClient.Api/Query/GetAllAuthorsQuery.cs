using GitHubClient.Api.Dtos;
using MediatR;

namespace GitHubClient.Api.Query
{
    public class GetAllAuthorsQuery: IRequest<List<string>>
    {
        public string Owner { get; set; }
        public string Repo { get; set; }
        public FilterDto Filter { get; set; }

        public GetAllAuthorsQuery(string owner, string repo)
        {
            Owner = owner;
            Repo = repo;
        }       
    }
}
