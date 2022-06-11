namespace pet;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

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
        return await _appointmentRepository.GetAppointmentsByUserId(userId);
    }

    public async Task<Appointment> GetAppointmentById(long appointmentId)
    {
        return await _appointmentRepository.GetAppointmentById(appointmentId);
    }

    public async Task<long> AddAppointment(Appointment appointment)
    {
        return await _appointmentRepository.AddAppointment(appointment);
    }

    public async Task EditAppointment(Appointment appointment)
    {
        await _appointmentRepository.EditAppointment(appointment);
    }
}