using System.Text.Json.Serialization;

namespace GitHubClient.Api.Models
{
    public class Author
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; } 
    } 
}
