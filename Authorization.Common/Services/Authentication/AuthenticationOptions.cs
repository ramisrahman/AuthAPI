
namespace Authorization.Common.Services.Authentication
{
    public class AuthenticationOptions
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public double ExpirationMinutes { get; set; }

    }
}
