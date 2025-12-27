using SmartCollege.Services.College.Models;

namespace SmartCollege.Services.College.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(AppUser user);
        RefreshToken GenerateRefreshToken();
    }
}
