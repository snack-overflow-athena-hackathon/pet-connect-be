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
        private readonly IAppointmentService _appointmentService;
        private readonly IPetService _petService;

        public UsersController(IUserService userService, IAppointmentService appointmentService, IPetService petService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _petService = petService;
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
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("{userId:long}/Appointments")]
        public async Task<ActionResult> GetAppointmentsByUserId(long userId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByUserId(userId);
                return Ok(appointments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("{userId:long}/Pets")]
        public async Task<ActionResult> GetPetsByUserId(long userId)
        {
            try
            {
                var returnedPets = await _petService.GetPetsByUserId(userId);
                return Ok(returnedPets);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            try
            {
                var id = await _userService.AddUser(user);
                user.Id = id;

                return Created($"{id}", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditUser([FromBody] User user)
        {
            try
            {
                await _userService.EditUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}