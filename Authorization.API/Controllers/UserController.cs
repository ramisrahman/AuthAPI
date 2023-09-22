using Authorization.Common.Helpers.Controllers.User;
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
        public async Task<ActionResult> Register()
        {
            await _userHelper.RegisterUser();
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login()
        {
            throw new NotImplementedException();
        }
    }
}
