namespace pet;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAppointments();
    Task<IEnumerable<Appointment>> GetAppointmentsByUserId(long userId);
    Task<Appointment> GetAppointmentById(long appointmentId);
    Task<long> AddAppointment(Appointment appointment);
    Task EditAppointment(Appointment appointment);
}