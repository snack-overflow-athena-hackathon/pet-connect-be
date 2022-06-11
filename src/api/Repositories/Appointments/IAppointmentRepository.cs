namespace pet;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAppointments();
}