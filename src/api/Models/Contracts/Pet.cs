namespace pet;

public class Pet
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public string OwnerDisplayName { get; set; }
    public long TypeId { get; set; }
    public string Animal { get; set; }
    public string? Breed { get; set; }
    public string PetName { get; set; }
    public string? Gender { get; set; }
    public string? Bio { get; set; }
    public string? PictureUrl { get; set; }
    public int ListOrder { get; set; }
    public bool Deactivated { get; set; }
}

public class Reviews
{
    public long Id { get; set; }
    public long PetId { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; }
    public string Review { get; set; }
}