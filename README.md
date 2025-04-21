# Trivia Game

A Windows trivia game built with .NET Framework 4.7.2 and WPF. Users sign up, log in, and answer questions with one correct and one incorrect option. Stats and scores are tracked using a local SQL Server database.

## Features
- User signup and login
- API-based trivia questions (Open Trivia DB)
- Score tracking: +1 correct, –1 incorrect
- Local SQL Server database with Entity Framework (DB-first)
- Fixed-size, centered WPF UI

## Demo
Watch the demo video here:  
[Demo Video](https://youtu.be/bIW2UX4PA7U)

## Dependencies
- .NET Framework 4.7.2  
- Visual Studio 2022  
- Newtonsoft.Json  
- Entity Framework 6  
- SQL Server (local instance)

## Database Schema

```sql
CREATE TABLE [User] (
    id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    score INT DEFAULT 0,
    total_attempts INT DEFAULT 0,
    total_correct INT DEFAULT 0,
    total_incorrect INT DEFAULT 0
);
```
