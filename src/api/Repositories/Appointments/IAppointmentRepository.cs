using Microsoft.AspNetCore.Mvc;

namespace pet;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAppointments();
}