namespace pet;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAppointments();
    Task<IEnumerable<Appointment>> GetAppointmentsByUserId(long userId);
    Task<Appointment> GetAppointmentById(long appointmentId);
}