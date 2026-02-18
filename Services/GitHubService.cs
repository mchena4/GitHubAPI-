using System.Net.Http.Json; 
using System.Text.Json;
using GitHubApi.Models;

namespace GitHubApi.Services;

public class GitHubService 
{
    // Class HttpClient
    private readonly HttpClient _httpClient;
    // Constructor
    public GitHubService()
    {
        // Initialize HttpClient
        _httpClient = new HttpClient();
        // Set default request headers
        _httpClient.DefaultRequestHeaders.Add("User-Agent","GitHubFetcher");
    }
    // Get user profile information from GitHub API
    public async Task<UserProfile> GetUser(string username)
    {
        // API url
        string url = $"https://api.github.com/users/{username}";
        try {
            // Send GET request to GitHub API
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                // Handle status errors
                Console.WriteLine($"Error. Status Code:{response.StatusCode}");
                return null;
            }
            // Deserialize response content to UserProfile
            return await response.Content.ReadFromJsonAsync<UserProfile>();
        }
        catch (Exception ex)
        // Catch exceptions
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return null;
        }
    }
    // Get user repositories from GitHub API
    public async Task<List<RepositoryModel>> GetRepositories(string username)
    {
        string url = $"https://api.github.com/users/{username}/repos";
        try
        {
            // Send GET request to GitHub API
            var response = await _httpClient.GetAsync(url);
            // Check if the response is successful
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error. Status Code:{response.StatusCode}");
                // Return empty list
                return new List<RepositoryModel>();
            }
            
            // Return list of repositories 
            var repos = await response.Content.ReadFromJsonAsync<List<RepositoryModel>>();
            return repos ?? new List<RepositoryModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return new List<RepositoryModel>();
        }
    }
    // Get user starred repositories from GitHub API
    public async Task<List<RepositoryModel>> GetRepositoriesStarred(string username)
    {
        string url = $"https://api.github.com/users/{username}/starred";
        try
        {
            // Send GET request to GitHub API
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error. Status Code:{response.StatusCode}");
                return new List<RepositoryModel>();
            }
            var repos_starred = await response.Content.ReadFromJsonAsync<List<RepositoryModel>>();
            return repos_starred ?? new List<RepositoryModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return new List<RepositoryModel>();
        }
    }
}

