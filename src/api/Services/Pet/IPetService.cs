namespace pet;

public interface IPetService
{
    Task<IEnumerable<Pet>> GetPets();

    Task<Pet> GetPet(long petId);
}