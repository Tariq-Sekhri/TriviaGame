# TriviaGame
 
CREATE TABLE [User] (
    id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    score INT DEFAULT 0,
    attempted INT DEFAULT 0,
    correct INT DEFAULT 0,
    incorrect INT DEFAULT 0
);
