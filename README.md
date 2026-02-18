# GitHub Favorites API 

A Web API built with **ASP.NET Core** that allows you to search GitHub profiles and repositories, and save your favorite developers to a local database using **SQLite** and **Entity Framework Core**.

## Technologies Used

- **C# & .NET** (Web API)
- **Entity Framework Core** (ORM)
- **SQLite** (Local relational database)
- **Swagger / OpenAPI** (API documentation and testing)

## Main Features

- **GitHub Integration:** Consumes the public GitHub API to obtain real-time data.

- **Data Persistence:** Saves profiles to a local database (`app.db`).

- **Clean Architecture:** Uses Dependency Injection and the DTO (Data Transfer Object) pattern to separate the data layer from the public interface.

- **CORS Configured:** Ready to be consumed by any frontend application (such as React, Vue, or Vanilla JS).

## Prerequisites

Make sure you have the following installed on your system:
- [.NET SDK](https://dotnet.microsoft.com/download)

- [Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) installed globally. If you don't have them, run:

``bash
dotnet tool install --global dotnet-ef

## Create SQLite database
- dotnet ef database update

## Run application
- dotnet run

 ## Open Swagger to view API endpoints
 - http://localhost:5xxx/swagger

Developed with ❤️ to learn and build. 
