using System.ComponentModel.DataAnnotations;

namespace GitHubApi.DTOs;

public class FavoriteResponseDTO
{
    public int Id { get; set; }
    public string GitHubUsername { get; set; } 
    public string FullName { get; set; }       
    public string Link { get; set; }           
    public DateTime DateAdded { get; set; }
}