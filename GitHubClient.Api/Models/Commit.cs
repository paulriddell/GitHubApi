using System.Text.Json.Serialization;

namespace GitHubClient.Api.Models
{
    public class Commit
    {
        [JsonPropertyName("author")]
        public Author? Author { get; set; }
    }
}
