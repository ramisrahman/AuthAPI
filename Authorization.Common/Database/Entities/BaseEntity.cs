
namespace Authorization.Common.Database.Entities
{
    public class BaseEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
