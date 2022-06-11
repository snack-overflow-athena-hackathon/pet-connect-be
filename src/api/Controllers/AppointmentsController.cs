using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet;
using pet.Exceptions;
using Serilog;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;

        public AppointmentsController(IAppointmentService appointmentService, IUserService userService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAppointments()
        {
            try
            {
                var returnedAppointments = await _appointmentService.GetAppointments();
                return Ok(returnedAppointments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }   

        [HttpGet]
        [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("{appointmentId:long}")]
        public async Task<ActionResult> GetAppointmentById(long appointmentId)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentById(appointmentId);
                return Ok(appointment);
            }
            catch (AppointmentNotFoundException e)
            {
                Log.Error(e, $"Appointment {appointmentId} could not be retrieved");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("{appointmentId:long}/User")]
        public async Task<ActionResult> GetUserByAppointmentId(long appointmentId)
        {
            try
            {
                var user = await _userService.GetUserByAppointmentId(appointmentId);
                return Ok(user);
            }
            catch (AppointmentNotFoundException e)
            {
                Log.Error(e, $"Appointment {appointmentId} could not be retrieved");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Appointment), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAppointment([FromBody] Appointment appointment)
        {
            try
            {
                var id = await _appointmentService.AddAppointment(appointment);
                appointment.Id = id;

                return Created($"{id}", appointment);
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
        public async Task<ActionResult> EditAppointment([FromBody] Appointment appointment)
        {
            try
            {
                await _appointmentService.EditAppointment(appointment);
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