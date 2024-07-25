# JwtAuth.API

## About
 - ASP.NET Core Empty project
 - Has a dependency on the Core project (for password hashing, token generation, validation, entities and dtos, and eventually a logging service)
 - Contains the EF db context for database access (in a more complex scenario this would go in a separate infrastructure tier)
 - Contains endpoints that manipulate entities and dtos, talk to the database by issuing queries and commands, map dtos to entities and vice versa, perform validation for creates and updates, and return response results
 - Contains extension methods to help build the web application pipeline and map entities to dtos (and vice versa)
