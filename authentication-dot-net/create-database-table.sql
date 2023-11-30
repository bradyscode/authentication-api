USE UsersAuthentication

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Salt VARCHAR(255) NOT NULL,
    HashValue varbinary(MAX) NOT NULL,
    Permission INT NOT NULL DEFAULT 0
)