namespace pet.Exceptions
{
    public class AppointmentNotFoundException : ArgumentException
    {
        public AppointmentNotFoundException(long appointmentId) : base($"Appointment could not be found for id {appointmentId}") { }
    }
}