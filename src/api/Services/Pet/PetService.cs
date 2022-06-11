namespace pet;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;

    public PetService(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<IEnumerable<Pet>> GetPets()
    {
        return await _petRepository.GetPets();
    }
    
    public async Task<Pet> GetPetByPetId(long petId)
    {
        return await _petRepository.GetPetByPetId(petId);
    }

    public async Task<Pet> GetPetByUserId(long userId)
    {
        return await _petRepository.GetPetByUserId(userId);
    }
}