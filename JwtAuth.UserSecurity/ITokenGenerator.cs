namespace JwtAuth.UserSecurity;

public interface ITokenGenerator
{
    /// <summary>
    /// Generates a new authentication token.
    /// </summary>
    /// <param name="userId">The user's userId.</param>
    /// <param name="username">The user's username.</param>
    /// <returns>A string that is the user's authentication token.</returns>
    string GenerateToken(int userId, string username);

    /// <summary>
    /// Generates a new authentication token.
    /// </summary>
    /// <param name="userId">The user's userId.</param>
    /// <param name="username">The user's username.</param>
    /// <param name="roles">A collection of the user's roles.</param>
    /// <returns>A string that is the user's authentication token.</returns>
    string GenerateToken(int userId, string username, IEnumerable<string> roles);
}
