using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.BusinessLogic.UserManagement.Services;

public class TokenService : ITokenService
{
    private readonly string _jwtSigningKey;
    private readonly string _jwtAudience;
    private readonly string _jwtIssuer;

    public TokenService(IConfiguration configuration)
    {
        _jwtSigningKey = configuration["JWT:SigningKey"]!;
        _jwtAudience = configuration["JWT:Audience"]!;
        _jwtIssuer = configuration["JWT:Issuer"]!;
    }

    public string GenerateAccessToken(User user, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSigningKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, role),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtIssuer,
            Audience = _jwtAudience,
            Expires = DateTime.UtcNow.AddMinutes(Constants.JWTExpiryTime.Minutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            ),
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}
