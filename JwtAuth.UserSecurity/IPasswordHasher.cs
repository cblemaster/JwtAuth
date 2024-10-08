﻿namespace JwtAuth.UserSecurity;

/// <summary>
/// Represents a password hasher.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Given a clear text password, hash the password and return a Password Hash object.
    /// </summary>
    /// <param name="plainTextPassword">The password as typed in by the user.</param>
    /// <returns>A hashed password object.</returns>
    PasswordHash ComputeHash(string plainTextPassword);

    /// <summary>
    /// Verifies a match of an existing hashed password against a user input.
    /// </summary>
    /// <param name="existingHashedPassword">The existing hashed password.</param>
    /// <param name="plainTextPassword">The password as typed in by the user.</param>
    /// <param name="salt">The salt used to compute the original hash.</param>
    /// <returns>A bool indicating success or failure.</returns>
    bool VerifyHashMatch(string existingHashedPassword, string plainTextPassword, string salt);
}
