using Microsoft.AspNetCore.Mvc;
using GitHubApi.Services;
using GitHubApi.Models;

namespace GitHubApi.Controllers;

[ApiController] // Marks the class as an API controller
[Route("api/[controller]")] // Sets the route for the controller

public class GitHubController : ControllerBase
{

    private readonly GitHubService _service;

    // Constructor with dependency injection
    // Asp.Net injects the GitHubService into the controller
    public GitHubController(GitHubService service)
    {
        _service = service;
    }

    // GET Api/github/{username}
    [HttpGet("{username}")]
    // Get user statistics
    public async Task<IActionResult> GetUserStats(string username)
    {
        // Get user profile
        var userProfile = await _service.GetUser(username);
        
        // Not found case
        if(userProfile == null)
        {
            return NotFound($"User '{username}' not found.");
        }

        // Get repo from the user
        var repos = await _service.GetRepositories(username);

        // Create a response object with user profile, total repositories, and popular repositories
        var response = new 
        {
            Profile = userProfile,
            TotalRepositories = repos.Count,
            PopularRepos = repos.Where(r => r.Stars > 0).OrderByDescending(r => r.Stars)
        };

        return Ok(response);
    }


    
}

