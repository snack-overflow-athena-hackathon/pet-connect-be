namespace pet;

public interface IPetRepository
{
    Task<IEnumerable<Pet>> GetPets();
}