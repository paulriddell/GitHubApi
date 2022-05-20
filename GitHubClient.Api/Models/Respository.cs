using System.Text.Json.Serialization;

namespace GitHubClient.Api.Models
{
    public class Repository
    {
        [JsonPropertyName("commit")]
        public Commit? Commit { get; set; }
    }
}
