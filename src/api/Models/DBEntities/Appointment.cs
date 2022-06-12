namespace pet.Models.DBEntities;

public class AppointmentDBEntity
{
    public long Id { get; set; }
    public DateTime AppointmentDateTimeUTC { get; set; }
    public long OwnerId { get; set; }
    public long VisitorId { get; set; }
    public long PetId { get; set; }
    public string PetName { get; set; }
    public long LocationId { get; set; }
    public long AppointmentState { get; set; }

    public string OwnerFirstName { get; set; }
    public string OwnerPreferredName { get; set; }
    public string VisitorFirstName { get; set; }
    public string VisitorPreferredName { get; set; }
}