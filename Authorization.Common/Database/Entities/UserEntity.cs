
namespace Authorization.Common.Database.Entities
{
    public class UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PasswordHash { get; set; }
    }
}
