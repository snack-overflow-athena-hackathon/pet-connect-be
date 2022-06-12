namespace pet;

public class Appointment
{
    public long Id { get; set; }
    public DateTime AppointmentDateTimeUTC { get; set; }
    public long OwnerId { get; set; }
    public long VisitorId { get; set; }
    public long PetId { get; set; }
    public long LocationId { get; set; }
    public long AppointmentState { get; set; }
    public AppointmentState State { get; set; }
}

public class AppointmentState
{
    public long Id { get; set; }
    public string CurrentState { get; set; }
}