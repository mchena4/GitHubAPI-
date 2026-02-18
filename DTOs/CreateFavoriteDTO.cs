using System.ComponentModel.DataAnnotations;

namespace GitHubApi.DTOs;

public class CreateFavoriteDTO
{
    [Required] // Indicates that the Username field is required
    public string Username { get; set; } = string.Empty;
}