
namespace Authorization.Common.Models.Response
{
    public class LoginResponse
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public Guid UserId { get; set; }
        public string? EmailAddress { get; set; }
    }
}
