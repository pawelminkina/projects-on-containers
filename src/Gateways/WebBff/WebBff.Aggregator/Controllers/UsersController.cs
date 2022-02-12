using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Extensions;
using WebBff.Aggregator.Models.User;
using WebBff.Aggregator.Services.User;

namespace WebBff.Aggregator.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IUsersService usersService, IHttpContextAccessor httpContextAccessor)
        {
            _usersService = usersService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var organizationId = _httpContextAccessor.HttpContext.User.GetOrganizationId();
            var users = await _usersService.GetUsers(organizationId);
            return Ok(users);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] string id)
        {
            var user = await _usersService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserDto>> GetUserByUsername([FromRoute] string username)
        {
            var user = await _usersService.GetUserByUsername(username);
            return Ok(user);
        }

        [HttpGet("emailavailability")]
        public async Task<ActionResult<bool>> CheckEmailAvailability([FromQuery] string email)
        {
            var available = await _usersService.CheckEmailAvailability(email);
            return Ok(available);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserForCreationDto dto)
        {
            var newUserId = await _usersService.CreateUser(dto);
            return CreatedAtAction(nameof(GetUserById), new {id = newUserId}, new {id = newUserId});
        }

        [HttpPut("changepassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            await _usersService.ChangePassword(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] string id)
        {
            await _usersService.DeleteUser(id);
            return NoContent();
        }


    }
}
