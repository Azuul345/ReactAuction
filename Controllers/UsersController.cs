using Microsoft.AspNetCore.Mvc;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public async Task<ActionResult<LoginResponse>> Login(UserLoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid credentials or inactive user" });
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }



        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var success = await _userService.ChangePasswordAsync(
                userId,
                request.CurrentPassword,
                request.NewPassword);

            if (!success)
            {
                return BadRequest(new { message = "Current password is incorrect." });
            }

            return NoContent();
        }

        [HttpPatch("{id}/deactivateUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var success = await _userService.DeactivateUserAsync(id);

            if (!success)
            {
                return NotFound(new { message = "User not found." });
            }
            return NoContent();
        }

    }
}