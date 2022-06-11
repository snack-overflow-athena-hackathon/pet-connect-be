using Microsoft.AspNetCore.Mvc;

namespace pet;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAppointments();
}