using Microsoft.EntityFrameworkCore;
using GitHubApi.Models;

namespace GitHubApi.Data;

// DbContext for the application
public class AppDbContext : DbContext 
{
    // Constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // FavoriteUsers table
    public DbSet<FavoriteUser> FavoriteUsers { get; set; }
}