namespace SmartCollege.Services.College.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
