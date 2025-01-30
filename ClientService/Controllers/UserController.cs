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

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AppUser user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(AppUser user)
        {
            await _userService.UpdateUserAsync(user);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
