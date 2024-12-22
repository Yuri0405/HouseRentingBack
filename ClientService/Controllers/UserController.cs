using ClientService.Models;
using ClientService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser([FromQuery] UserQueryParams parameters)
        {
            var result = await _userService.GetUserAsync(parameters);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
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
