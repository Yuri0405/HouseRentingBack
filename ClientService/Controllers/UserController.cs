using ClientService.Models;
using ClientService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtAuthService _authService;

        public UserController(UserService userService, JwtAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register(AppUser user)
        {

            await _userService.AddUserAsync(user);

            return Ok($"{user.Name} registered!");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(AppUser user)
        {
            var token = await _authService.Authenticate(user);
            
            if (token == null)
            {
                return NotFound("Wrong password or username");
            }

            return Ok(token);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetUser([FromQuery] UserQueryParams parameters)
        {
            var result = await _userService.GetUserAsync(parameters);
            if (!result.Success)
            {
                return NotFound(new { message = result.Message });
            }
            return Ok(new {message = result.Message, user = result.Data});
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AppUser user)
        {
            var result = await _userService.AddUserAsync(user);
            
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            
            return Ok(new {message = result.Message});
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(AppUser user)
        {
            var result = await _userService.UpdateUserAsync(user);
            
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            
            return Ok(new {message = result.Message});
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            
            return Ok(new {message = result.Message});
        }
    }
}
