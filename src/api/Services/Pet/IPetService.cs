namespace pet;

public interface IPetService
{
    Task<IEnumerable<Pet>> GetPets();

    Task<Pet> GetPetByPetId(long petId);
    Task<Pet> GetPetByUserId(long userId);
}