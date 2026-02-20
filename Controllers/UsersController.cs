using Microsoft.AspNetCore.Mvc;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;


namespace ReactAuction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // POST: /api/users/register
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(UserRegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request);

            if (result == null)
            {
                return BadRequest(new { message = "Email is already in use." });
            }
            return Ok(result);
        }


        // POST: /api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(UserLoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid credentials or inactive user" });
            }

            return Ok(result);
        }


    }
}