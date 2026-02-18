using Microsoft.EntityFrameworkCore;
using GitHubApi.Data;
using GitHubApi.Services;

var builder = WebApplication.CreateBuilder(args);


// CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Web permission
              .AllowAnyMethod()  // Allows GET, POST, DELETE, etc.
              .AllowAnyHeader(); // Allows sending JSON
    });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

// Add GitHubService 
builder.Services.AddSingleton<GitHubService>();

// Add DbContext with Sqlite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Build the application
var app = builder.Build();

// Use cors
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
