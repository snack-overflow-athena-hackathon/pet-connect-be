namespace pet;

public interface IPetService
{
    Task<IEnumerable<Pet>> GetPets();
    Task<Pet> GetPetByPetId(long petId);
    Task<IEnumerable<Pet>> GetPetsByUserId(long userId);
    Task<long> AddPet(Pet pet);
    Task EditPet(Pet pet);
}