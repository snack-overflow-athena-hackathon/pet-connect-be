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
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var returnedUsers = await _userService.GetUsers();
                return Ok(returnedUsers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("{userId}")]
        public async Task<ActionResult> GetUserByUserId(long userId)
        {
            try
            {
                var returnedUser = await _userService.GetUserByUserId(userId);
                return Ok(returnedUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("Pet/{petId}")]
        public async Task<ActionResult> GetUserByPetId(long petId)
        {
            try
            {
                var returnedUser = await _userService.GetUserByPetId(petId);
                return Ok(returnedUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}