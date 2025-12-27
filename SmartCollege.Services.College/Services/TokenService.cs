
using Microsoft.IdentityModel.Tokens;
using SmartCollege.Services.College.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartCollege.Services.College.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateAccessToken(AppUser user)
        {
            var jwt = _config.GetSection("JwtSettings");

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"]!));

            var token = new JwtSecurityToken(
                jwt["Issuer"],
                jwt["Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials:
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.Now.AddDays(7)
            };
        }
    }
}
