using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public ActionResult Register()
        {
            throw new NotImplementedException();
            // Validate and register the user
            // Hash the password and save it to the database
            // Return a success message or error
        }

        [HttpPost("login")]
        public ActionResult Login()
        {
            throw new NotImplementedException();
            // Validate credentials
            // Generate and return a JWT token and refresh token upon successful login
        }
    }
}
