using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Core.Authentication;

/// <summary>
/// The jwt generator provides functionality to create an authentication
/// token for a given user.
/// </summary>
public class JwtGenerator(string secret) : ITokenGenerator
{
    private readonly string JwtSecret = secret;

    /// <summary>
    /// Generates a new authentication token.
    /// </summary>
    /// <param name="userId">The user's userId.</param>
    /// <param name="username">The user's username.</param>
    /// <returns>A string that is the user's authentication token.</returns>
    public string GenerateToken(int userId, string username) =>
        GenerateToken(userId, username, []);

    /// <summary>
    /// Generates a new authentication token.
    /// </summary>
    /// <param name="userId">The user's userId.</param>
    /// <param name="username">The user's username.</param>
    /// <param name="roles">A collection of the user's roles.</param>
    /// <returns>A string that is the user's authentication token.</returns>
    public string GenerateToken(int userId, string username, IEnumerable<string> roles)
    {
        List<Claim> claims =
        [
            new Claim("sub", userId.ToString()),
            new Claim("name", username),
        ];

        roles.ToList().ForEach(r =>
        {
            if (!string.IsNullOrWhiteSpace(r))
            {
                claims.Add(new Claim(ClaimTypes.Role, r.Trim()));
            }
        });

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSecret)),
                SecurityAlgorithms.HmacSha256Signature),
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
