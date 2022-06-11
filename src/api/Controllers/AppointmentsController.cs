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
            var returnedAppointments = await _appointmentService.GetAppointments();

            return Ok(returnedAppointments);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult> GetAppointmentsByUser(long userId)
        {
            return null;
        }
    }
}