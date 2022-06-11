namespace pet;

public interface IPetService
{
    Task<IEnumerable<Pet>> GetPets();
}