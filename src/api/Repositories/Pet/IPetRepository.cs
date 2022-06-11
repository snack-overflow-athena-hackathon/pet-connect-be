namespace pet;

public interface IPetRepository
{
    Task<IEnumerable<Pet>> GetPets();
    Task<Pet> GetPet(long petId);
}