using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
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
        [Route("{userId}")]
        public async Task<ActionResult> GetAppointmentsByUser(long userId)
        {
            try
            {
                var returnedPet = await _appointmentService.GetAppointmentsByUserId(userId);
                return Ok(returnedPet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}