[![Build & Test](https://github.com/bradyscode/authentication-api/actions/workflows/workflow.yml/badge.svg)](https://github.com/bradyscode/authentication-api/actions/workflows/workflow.yml) [![Dependency Check](https://github.com/bradyscode/authentication-api/actions/workflows/dependencyCheck.yml/badge.svg)](https://github.com/bradyscode/authentication-api/actions/workflows/dependencyCheck.yml)

- [Authentication API](#authentication-api)
- [Authentication](#authentication)
- [Setup](#setup)
  * [Prerequisites](#prerequisites)
  * [Database Setup](#database-setup)
  * [Configuration](#configuration)
    + [Connection String](#connection-string)
    + [JWT Settings](#jwt-settings)
  * [Troubleshooting](#troubleshooting)
- [Endpoints](#endpoints)
  * [/CreateUser](#-createuser)
    + [_Description:_](#-description--)
    + [Request Body](#request-body)
    + [Response Body](#response-body)
  * [/AuthenticateUser](#-authenticateuser)
    + [_Description:_](#-description---1)
    + [Request Body](#request-body-1)
    + [Response Body](#response-body-1)
- [Data Models](#data-models)
  * [User](#user)
  * [UserDTO (Data Transfer Object)](#userdto--data-transfer-object-)
  * [Password](#password)
  * [Permission Enum](#permission-enum)
- [Deploying the API and Database with Docker Compose](#deploying-the-api-and-database-with-docker-compose)
  * [Docker Compose Configuration](#docker-compose-configuration)
  * [Deployment Steps](#deployment-steps)
  * [Accessing the API](#accessing-the-api)
  * [Connecting to SQL Server](#connecting-to-sql-server)
    + [Change default password!!!](#note-)
- [Authentication API Roadmap](#authentication-api-roadmap)
  * [Phase 1: Core Authentication Functionality](#phase-1--core-authentication-functionality)
  * [Phase 2: Enhanced Security and Integration](#phase-2--enhanced-security-and-integration)
  * [Phase 3: Scalability and Performance Optimization](#phase-3--scalability-and-performance-optimization)
- [How to implement authentication to an endpoint (open source users)](#how-to-implement-authentication-to-an-endpoint--open-source-users-)
- [References](#references)

<small><i><a href='http://ecotrust-canada.github.io/markdown-toc/'>Table of contents generated with markdown-toc</a></i></small>


# Authentication API

An authentication API is a crucial component of any secure application, providing a mechanism for users to prove their identity and access authorized resources. It typically involves a user registration process where users create an account and provide credentials such as a username and password. Upon subsequent login attempts, users submit their credentials to the API, which validates them against the stored information. If the credentials are valid, the API issues an authentication token, which serves as a proof of identity for the user during subsequent interactions with the application. This token allows the user to access authorized resources without having to repeatedly provide their credentials, ensuring a seamless and secure user experience.

An authentication API typically employs various security measures to protect user credentials and prevent unauthorized access. These measures may include password hashing, token expiration, and secure communication protocols. By implementing robust authentication mechanisms, applications can safeguard sensitive user data and maintain a high level of security.

# Authentication

When a user attempts to log in to the C# REST API, they provide their username and password. The API first retrieves the user's salt from the database. Then, it hashes the input password using the same hashing algorithm and salt combination that was used when the password was originally stored. Finally, the API compares the computed hash to the stored hash. If the hashes match, the user is authenticated and granted access to the API. This process ensures that passwords are never stored in plain text and that even if an attacker gains access to the database, they will not be able to recover the user's password.

# Setup

## Prerequisites

- [Database Management Tool](https://www.forbes.com/sites/forbestechcouncil/2020/01/08/12-top-recommended-database-management-tools/?sh=1baca4d73cd3)
- [Docker](https://www.docker.com/get-started/)

## Database Setup

Database setup has been automated, when running the application it will check your connection string settings in appsettings.json and edit run queries to check if the database and table exist.

```json
  "ConnectionStrings": {
    "Database": {
      "Server": "localhost, 1433",
      "Database": "UsersAuthentication",
      "UserId": "{SetFromPasswordVault}",
      "Password": "{SetFromPasswordVault}",
      "MultipleActiveResultSets": true
    }
  }
```

## Configuration

### Connection String

If you are running this API locally or on your own cloud environment you need to edit the configuration, in _appsettings.json_, you will want to change the connection string settings.

```json
  "ConnectionStrings": {
    "Database": {
      "Server": "localhost, 1433",
      "Database": "UsersAuthentication",
      "UserId": "{SetFromPasswordVault}",
      "Password": "{SetFromPasswordVault}",
      "MultipleActiveResultSets": true
    }
  }
```

### JWT Settings

In the _appsettings.json_ file there is a section for JWT configuration, you will want to change everything in here

- Issuer: The party that "created" the token and signed it with it's private key. _Anyone can create tokens, make sure that the tokens you receive is created by a party that you trust._
- Audience: In the JSON Web Token (JWT) standard, the "aud" (audience) claim is a string or array of strings that identifies the recipients that the JWT is intended for. This can be a specific application, a set of applications, or the general public. The "aud" claim is used to prevent JWT token abuse, and to provide some basic information about the context in which the JWT was issued. The "aud" claim is a mandatory claim, and must be included in every JWT. The value of the "aud" claim must be a string or array of strings that is registered with the authorization server. The authorization server can then use the "aud" claim to verify the validity of the JWT, and to determine which client or clients are allowed to access the protected resources.
- Key: In the JSON Web Token (JWT) standard, the "kid" (key ID) claim is a string that indicates the key that was used to digitally sign the JWT. This is used to verify the authenticity and integrity of the JWT, and to prevent JWT token abuse
  <br/>

Notes: The key used in this documentation is randomly generated for example purposes, Do not use this for the production environment.
```json
  "Jwt": {
    "Issuer": "Issuer",
    "Audience": "Audience",
    "Key": "bd1a1ccf8095037f361a4d351e7c0de65f0776bfc2f478ea8d312c763bb6caca"
  }
```

## Troubleshooting

For any troubleshooting needs please feel free to shoot me an email bradystohler15@gmail.com or go to my [website](http://bradystohler.com) and use the "Contact Me" section.

# Endpoints

## /CreateUser

### _Description:_

Creates a new user in the database.

### Request Body

```json
{
    "username":"username",
    "password":"ASuperComplexPassword123!@456",
    "permission":1 //1 for admin, 2 for user
}
```

### Response Body

```json
User Created
```

## /AuthenticateUser

### _Description:_

This endpoint validates the provided username and password against the corresponding hash and salt stored in the database. Upon successful authentication, a JSON Web Token (JWT) is issued, granting access to authorized resources. Otherwise, an Unauthorized response is returned to indicate invalid credentials.

### Request Body

```json
{
    "username":"username",
    "password":"ASuperComplexPassword123!@456",
    "permission":1 //1 for admin, 2 for user
}
```

### Response Body

- Authentication successful
```jwt
eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0MSIsImVtYWlsIjoidGVzdDEiLCJuYmYiOjE3MDExMzQ1MjIsImV4cCI6MTcwMTEzNTEyMiwiaWF0IjoxNzAxMTM0NTIyLCJpc3MiOiJJc3N1ZXIiLCJhdWQiOiJBdWRpZW5jZSJ9.PiSQEN48hYVSIQjrRr_98_vGoYRDQdF2llMS2WN3B7YTQPSgGRMkrFZpOnp9qqaaJpayqr4x94WwWiYhvbxQ0Q
```
- Authentication unsuccessful

[401 - Http Response](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401)

# Data Models

## User

```cs
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = HashPassword(password);
            Permission = Permission.USER;
        }

        public User(string username, string password, int permission)
        {
            Username = username;
            Password = HashPassword(password);
            Permission = (Permission)permission;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public Password Password { get; set; }
        public Permission Permission { get; set; }
    }
```

## UserDTO (Data Transfer Object)

```cs
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Permission { get; set; }
    }
```

## Password
```cs
    public class Password
    {
        public Password()
        {
            //here for when pulling and parsing from db
        }
        public Password(byte[] hashValue, string salt)
        {
            HashValue = hashValue;
            Salt = salt;
        }

        public string Salt { get; set; }
        public byte[] HashValue { get; set; }
    }
```

## Permission Enum

```cs
    public enum Permission
    {
        USER,
        ADMIN
    }
```

# Deploying the API and Database with Docker Compose

To simplify the deployment and management of your API and database, this project utilizes Docker Compose. Docker Compose provides a convenient way to define and run multi-container Docker applications. By defining your application's services and their configurations in a YAML-based file, you can easily launch and manage all of your containers with a single command.

## Docker Compose Configuration

The `docker-compose.yml` file defines two services:

`authentication-dot-net`: This service builds the Docker image for your API application and runs it in a container named `web_api_application`. It exposes port 80 of the container to port 5000 of the host machine, allowing you to access the API externally.

`sql`: This service uses the official Microsoft SQL Server image (`mcr.microsoft.com/mssql/server:2022-latest`) and runs it in a container named `sql_server2022`. It sets the `ACCEPT_EULA` and `SA_PASSWORD` environment variables to accept the SQL Server license agreement and set the SQL Server SA password to `A&VeryComplex123Password`, respectively.

## Deployment Steps

1. Build Docker Images: To build the Docker images for your API and database, execute the following command from the project directory:
```bash
docker-compose build
```

2. Start Docker Containers: To start the API and database containers, execute the following command:
```bash
docker-compose up -d
```

This command will start both the `authentication-dot-net` and `sql` containers in background mode.

## Accessing the API

Once the containers have started, you can access your API using the following URL:

http://localhost:5000
This will connect you to the API running in the web_api_application container.

## Connecting to SQL Server

To connect to the SQL Server instance running in the `sql_server2022` container, you can use the following connection string:

```json
Server=localhost,1433;Database=master;Uid=sa;Pwd=A&VeryComplex123Password;
```
This connection string uses the default SQL Server SA username and password set during container startup.

### Note:
The password is set by defualt to `A&VeryComplex123Password`, please change this before creating the docker container.

# Authentication API Roadmap

## Phase 1: Core Authentication Functionality

- <del>Establish user authentication mechanisms: Implement user registration, login,</del> and logout endpoints.

- <del>Secure password storage: Store user passwords using a secure hashing algorithm and unique salts to prevent plain-text storage and unauthorized access.

- <del>Implement token-based authentication: Utilize JSON Web Tokens (JWTs) to maintain user authentication sessions and provide secure access to protected resources.

- Enable role-based access control (RBAC): Define user roles and permissions to restrict access to specific API endpoints based on user authorization levels.

## Phase 2: Enhanced Security and Integration

- Adopt OAuth 2.0 protocol: Integrate with external OAuth providers to enable seamless authentication and authorization for third-party applications.

- Support OpenID Connect (OIDC): Implement OpenID Connect as an identity layer to provide user profile data and claims for personalized experiences.

## Phase 3: Scalability and Performance Optimization -

- Implement caching mechanisms: Utilize caching strategies to improve API response times and reduce database load.

- Optimize resource utilization: Employ techniques to optimize memory usage, CPU usage, and database queries for efficient API performance.

- Monitor and analyze API usage: Implement monitoring tools to track API usage patterns, identify bottlenecks, and optimize performance accordingly.

- Consider cloud-based deployment: Explore cloud-based hosting solutions to scale the API infrastructure and handle increased traffic.

# How to implement authentication to an endpoint (open source users)

![AuthentcationToEndpoint](images/HowToImplementAuthenticationToEndppoint.png)

# References

[Mojo Auth](https://mojoauth.com/glossary/)
