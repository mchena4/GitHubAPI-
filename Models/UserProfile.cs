using System.Text.Json.Serialization;

namespace GitHubApi.Models;

public class UserProfile
{
    // Convert GitHub API response to UserProfile
    [JsonPropertyName("login")]
    public string? Username { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("bio")]
    public string? Bio { get; set; }

    [JsonPropertyName("public_repos")]
    public int PublicRepos { get; set; }
    
    [JsonPropertyName("followers")]
    public int Followers { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
