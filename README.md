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
 - Values for these fields must be unique (currently only enforced by the web api):
	- User.Username
	- User.Email
	- User.Phone

## Business rules
 - Plain text passwords are not persisted to the database, but plain text passwords entered by a user have a maximum length of 50 characters
 - A User must have at least one (1) Role
 - Create date is meant to be set when a record is inserted, then never changed (TBD - how to enforce this in the db and/or app?)
 - Update date is meant to be set every time a record is updated  (TBD - how to enforce this in the db and/or app?)

## Project overview
### Core
### DataClient
### MAUI (targeting windows desktop only)
### UserSecurity
### Web

## Instructions for running the application
 - Clone or download the repo
 - Run the script to create the database (\JwtAuth\Database\JwtAuth-Create-Db-Script.sql)
 - Run the solution file with Visual Studio (\JwtAuth\JwtAuth.sln)

## Improvement opportunities
 - Keep project .NET versions up to date
 - Keep nuget package versions up to date
 - Move data validation in api endpoints (web project) into endpoint filters
 - Add authorization to endpoints: change user password, change user roles, update user profile
 - Better validation for unique username, email, and phone
 - MAUI improvements:
	- Role collectionviews' selected items styling is not working
	- Clear register and login pages when revisited
	- User details page, fix show/hide token switch styling and vertical alignment
	- Fix nav bar styling
	- User details page does not display update date correctly when value is null
	- User details page show comma separated roles with a space after each comma
	- Show/hide for password entries
	- Button enabling/disabling for all buttons
	- Tab order and focus
 - New UI projects
 - Apply ASP.NET Core best practices from Microsoft in web project: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-8.0
