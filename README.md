# JwtAuth

## About
 - An application for managing user authentication and authorization with JWTs
 - With support for role-based authentication
 - Application architecture visuals: \JwtAuth\Documentation

## Built with
 - NET 8 / C# 12
 - SQL server database
 - Entity Framework Core 8
 - FluentValidation https://fluentvalidation.net

## Features
 - Register as a new user
 - Login as an existing user
 - Change a user's password
 - Change a user's role(s)
 - Update a user's 'profile' (non-authentication related info)

## My objectives
 - The solution will be a reusable template for my apps that need user identity management
 - The solution will have separation of concerns (ask each class what its job is)
 - The solution will be clean code that is as 'DRY' as I can get it
 - The solution will use the FluentValidation library (which I haven't used before, so I will learn something new)
 - The solution will include logging, maybe from an external library (none of my apps has ever included logging, so I'm learning something new here too)

## Database overview
 - There is a script to create the SQL server database (\JwtAuth\Database\JwtAuth-Create-Db-Script.sql)
 - There is one (1) database table
	- User: username and encrypted password, first name, last name, email, phone, roles that can be used in authorization schemes
	
## Database rules
 - Values for these fields are required and have a maximum length:
	- User.Username (50)
	- User.FirstName (255)
	- User.LastName (255)
	- User.Email (255)
	- User.Phone (10)
 - Values for these fields must be unique:
	- User.Username
	- User.Email
	- User.Phone

## Business rules
 - Plain text passwords are not persisted to the database, but plain text passwords entered by a user have a maximum length of 50 characters
 - A User must have at least one (1) Role
 - Create date is meant to be set when a record is inserted, then never changed (TBD - how to enforce this in the db and/or app?)
 - Update date is meant to be set every time a record is updated  (TBD - how to enforce this in the db and/or app?)

## UI conventions
 - TBD

## Project overview
### Core
### DataClient
### MAUI (targeting windows desktop only)
### UserSecurity
### Web

## Instructions for running the application
 - TBD

## Improvement opportunities
 - Keep project .NET versions up to date
 - Keep nuget package versions up to date
 - Move data validation in api endpoints (web project) into endpoint filters
 - Apply ASP.NET Core best practices from Microsoft in web project: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-8.0
 - New UI projects
