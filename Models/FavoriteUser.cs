namespace GitHubApi.Models;

public class FavoriteUser
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public DateTime AddedOn { get; set; } = DateTime.Now;
}