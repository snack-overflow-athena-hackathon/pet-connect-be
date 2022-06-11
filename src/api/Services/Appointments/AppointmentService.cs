using Microsoft.AspNetCore.Mvc;

namespace pet;

public class AppointmentService : IAppointmentService
{
    private IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<Appointment>> GetAppointments()
    {
        return await _appointmentRepository.GetAppointments();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByUserId(long userId)
    {
        return await _appointmentRepository.GetAppointmentsByUserId();
    }
}