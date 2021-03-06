namespace pet;

public class User
{
    public long Id { get; set; }
    public bool PetOwner { get; set; }
    public string? Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Pronouns { get; set; }
    public string? PreferredName { get; set; }
    public string Email { get; set; }
    public string? Bio { get; set; }
    public string? PictureUrl { get; set; }
    public bool Deactivated { get; set; }
    public string DisplayName { get; set; }
}