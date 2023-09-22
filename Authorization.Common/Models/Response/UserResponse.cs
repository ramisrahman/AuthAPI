
namespace Authorization.Common.Models.Response
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
