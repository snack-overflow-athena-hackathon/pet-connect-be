using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var returnedUsers = await _userService.GetUsers();

            return Ok(returnedUsers);
        }
    }
}