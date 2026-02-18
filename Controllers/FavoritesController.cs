using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GitHubApi.Data;
using GitHubApi.Services;
using GitHubApi.Models;
using GitHubApi.DTOs;

namespace GitHubApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FavoritesController : ControllerBase
{
    // Database context and Github service
    private readonly AppDbContext _dbContext;
    private readonly GitHubService _gitHubService;

    // Constructor with dependency injection
    public FavoritesController(AppDbContext context, GitHubService gitHubService)
    {
        _dbContext = context;
        _gitHubService = gitHubService;
    }

    [HttpGet] //GET api/controller
    // Get all favorite users
    public async Task<ActionResult<List<FavoriteResponseDTO>>> GetAllFavorites()
    {
        // Read favorite users from the database
        var favoriteUsers = await _dbContext.FavoriteUsers.ToListAsync();

        // Map list of users to a list of response DTO objects
        var responseDTOs = favoriteUsers.Select(u => new FavoriteResponseDTO
        {
            Id = u.Id,
            GitHubUsername = u.Username,
            FullName = u.Name,
            Link = u.Url,
            DateAdded = u.AddedOn
        }).ToList();

        return Ok(responseDTOs);
    }

    [HttpPost("{username}")] //POST api/controller/{username}
    
    // Add a favorite user to database
    public async Task<IActionResult> AddFavorite([FromBody] CreateFavoriteDTO request)
    {
        // Check if user is already in favorites
        var exists = await _dbContext.FavoriteUsers.AnyAsync(u => u.Username == request.Username);
        if (exists)
        {
            return BadRequest("User is already in favorites.");
        }

        // Get user profile from GitHub
        var userProfile = await _gitHubService.GetUser(request.Username);

        // Check if user profile was retrieved successfully
        if (userProfile == null)
        {
            return NotFound($"User '{request.Username}' not found on GitHub.");
        }

        // Create a new favorite user
        var newEntity = new FavoriteUser
        {
            Username = userProfile.Username,
            Name = userProfile.Name ?? "N/A",
            Url = userProfile.Url,
            AddedOn = DateTime.Now
        };

        // Add to database
        _dbContext.FavoriteUsers.Add(newEntity);
        await _dbContext.SaveChangesAsync();

        // Create response DTO with new favorite user details
        var responseDTO = new FavoriteResponseDTO
        {
            Id = newEntity.Id,
            GitHubUsername = newEntity.Username,
            FullName = newEntity.Name,
            Link = newEntity.Url,
            DateAdded = newEntity.AddedOn
        };

        // Return success response with created DTO
        return Ok(new { message = $"User {newEntity.Username} added to favorites!", data = responseDTO });
    }

    [HttpDelete("{username}")] // DELETE api/controller/{username}
    // Remove a favorite user from database
    public async Task<IActionResult> RemoveFavorite(string username)
    {
        // Find the user to delete
        var userToDelete = await _dbContext.FavoriteUsers.FirstOrDefaultAsync(u => u.Username == username);
        // Check if user exists in database
        if (userToDelete == null)
        {
            return NotFound($"User '{username}' not found in favorites.");
        }

        // Remove from database
        _dbContext.FavoriteUsers.Remove(userToDelete);
        await _dbContext.SaveChangesAsync();

        return Ok($"User '{username}' removed from favorites.");
    }

}