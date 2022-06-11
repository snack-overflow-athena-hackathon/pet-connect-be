namespace pet;

public interface IPetRepository
{
    Task<IEnumerable<Pet>> GetPets();
    Task<Pet> GetPetByPetId(long petId);
    Task<Pet> GetPetByUserId(long userId);
}