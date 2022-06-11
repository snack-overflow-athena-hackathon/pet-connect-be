namespace pet;

public class Pet
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public long TypeId { get; set; }
    public string? Breed { get; set; }
    public string Name { get; set; }
    public string? Bio { get; set; }
    public string? PictureUrl { get; set; }
    public int ListOrder { get; set; }
}