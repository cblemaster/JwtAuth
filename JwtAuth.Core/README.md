# JwtAuth.Core

## About
 - Class library project
 - Cross cutting concerns
 - (Almost) entirely public by design; the Core project is intended to be used by other projects
 - The Core project has no dependencies on other projects
 - Authentication tools: password hashing and token generation
 - Entities and dtos
 - Validation for create and update dtos
 - Data service that calls api endpoints (not yet complete)
 - Static class representing the logged in user - TBD
 - Logging service - TBD

## Data Transfer Objects (dtos)
 - Used to copy data from, or copy data to, entities
 - 'Get' dtos are immutable on purpose
 - 'Create' and 'Update' dtos, on the other hand, are easy to mutate (public properties and property accessors, no 'required' properties)
 - All dtos can be used for databinding in the UI layer
 - All dtos can be used as method parameters and return types in the API project
