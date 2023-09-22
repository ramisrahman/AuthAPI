using Authorization.Common.Models.Request;
using Authorization.Common.Models.Response;

namespace Authorization.Common.Helpers.Controllers.User
{
    public interface IUserHelper
    {
        Task<UserResponse> RegisterUserAsync(UserRequest request);
        Task<string> UserLoginAsync(UserRequest request);
    }
}
