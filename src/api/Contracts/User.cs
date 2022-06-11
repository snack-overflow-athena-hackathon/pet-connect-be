namespace pet;

public class User
{
    public long Id { get; set; }
    public long GenderId { get; set; }
    public bool PetOwner { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PreferredName { get; set; }
    public string? Bio { get; set; }
    public string? PictureUrl { get; set; }
    public string Email { get; set; }
}