using SmartCollege.Services.College.Models.Dto;

namespace SmartCollege.Services.College.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
        Task<LoginResponseDto?> RefreshAsync(string refreshToken);
    }
}
