using System.Security.Cryptography;

namespace JwtAuth.Core.Authentication;

/// <summary>
/// The password hasher provides functionality to hash a plain text password and validate
/// an existing password against its hash.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int WORK_FACTOR = 10000;

    /// <summary>
    /// Given a clear text password, hash the password and return a Password Hash object.
    /// </summary>
    /// <param name="plainTextPassword">The password as typed in by the user.</param>
    /// <returns>A hashed password object.</returns>
    public PasswordHash ComputeHash(string plainTextPassword)
    {
        //Create the hashing provider
        Rfc2898DeriveBytes rfc = new(plainTextPassword, 8, WORK_FACTOR, HashAlgorithmName.SHA256);

        //Get the hashed password
        byte[] hash = rfc.GetBytes(20);

        //Set the salt value
        string salt = Convert.ToBase64String(rfc.Salt);

        //Return the hashed password
        return new PasswordHash(Convert.ToBase64String(hash), salt);
    }

    /// <summary>
    /// Verifies a match of an existing hashed password against a user input.
    /// </summary>
    /// <param name="existingHashedPassword">The existing hashed password.</param>
    /// <param name="plainTextPassword">The password as typed in by the user.</param>
    /// <param name="salt">The salt used to compute the original hash.</param>
    /// <returns>A bool indicating success or failure.</returns>
    public bool VerifyHashMatch(string existingHashedPassword, string plainTextPassword, string salt)
    {
        byte[] saltArray = Convert.FromBase64String(salt);      //gets us the byte[] array representation

        //Create the hashing provider
        Rfc2898DeriveBytes rfc = new(plainTextPassword, saltArray, WORK_FACTOR, HashAlgorithmName.SHA256);

        //Get the hashed password
        byte[] hash = rfc.GetBytes(20);

        //Compare the hashed password values
        string newHashedPassword = Convert.ToBase64String(hash);

        return (existingHashedPassword == newHashedPassword);
    }
}
