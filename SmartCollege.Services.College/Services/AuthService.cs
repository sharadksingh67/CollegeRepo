using Microsoft.EntityFrameworkCore;
using SmartCollege.Services.College.Data;
using SmartCollege.Services.College.Models.Dto;

namespace SmartCollege.Services.College.Services
{
    public class AuthService : IAuthService
    {
        private readonly CollegeDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(CollegeDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<LoginResponseDto?> RefreshAsync(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(t => t.AppUser)
                .FirstOrDefaultAsync(t =>
                    t.Token == refreshToken &&
                    !t.IsRevoked &&
                    t.ExpiryDate > DateTime.Now);

            if (token == null) return null;

            token.IsRevoked = true;

            var newRefresh = _tokenService.GenerateRefreshToken();
            token.AppUser.RefreshTokens.Add(newRefresh);

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken =
                    _tokenService.GenerateAccessToken(token.AppUser),
                RefreshToken = newRefresh.Token
            };
        }
    }
}
