using System.Text.Json.Serialization;

namespace GitHubApi.Models;

public class RepositoryModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("html_url")]
    public string? Url { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("stargazers_count")]
    public int Stars { get; set; }
    [JsonPropertyName("language")]
    public string? Language { get; set; }
}
