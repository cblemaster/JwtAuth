# JwtAuth.Core

## About
 - Class library project
 - Cross cutting concerns 
 - Core project has no dependencies on other projects
 - Entities and dtos
 - Authentication tools service - password hashing, token generation
 - Validation service - for create and update dtos
 - Data service - call api endpoints
 - (?) Static logged in user class
 - (?) Db schema constants - static class
 - Logging service - TBD

## Data Transfer Objects (dtos)
 - Used to copy data from, or copy data to, entities
 - 'Get' dtos are immutable on purpose
 - 'Create' and 'Update' dtos, on the other hand, are easy to mutate (due to public properties and property accessors)
 - All dtos can be used for databinding in the UI layer ('get' dtos are of course readonly)
