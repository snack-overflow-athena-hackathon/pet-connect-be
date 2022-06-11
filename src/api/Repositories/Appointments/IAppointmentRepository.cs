namespace pet;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAppointments();
    Task<IEnumerable<Appointment>> GetAppointmentsNew();
    Task<IEnumerable<Appointment>> GetAppointmentsByUserId();
}