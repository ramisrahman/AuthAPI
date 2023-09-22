using Authorization.Common.Helpers.Controllers.User;
using Authorization.Common.Models.Request;
using Authorization.Common.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserHelper _userHelper;

        public UserController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> RegisterUserAsync(UserRequest request) =>
            Ok(await _userHelper.RegisterUserAsync(request));

        [HttpPost("login")]
        public async Task<ActionResult<string>> UserLoginAsync(UserRequest request) =>
            Ok(await _userHelper.UserLoginAsync(request));
    }
}
