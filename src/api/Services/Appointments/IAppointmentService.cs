using Microsoft.AspNetCore.Mvc;

namespace pet;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAppointments();
    Task<IEnumerable<Appointment>> GetAppointmentsByUserId(long userId);
}