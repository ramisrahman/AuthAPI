using Authorization.Common.Database.Entities;
using Authorization.Common.Models.Response;

namespace Authorization.Common.Extensions
{
    public static class UserExtensions
    {
        public static UserResponse ToResponse(this UserEntity entity)
        {
            return new UserResponse
            {
                CreatedAt = entity.CreatedAt,
                EmailAddress = entity.EmailAddress,
                UpdatedAt = entity.UpdatedAt,
                UserId = entity.Id,
                UserName = entity.UserName
            };
        }
    }
}
