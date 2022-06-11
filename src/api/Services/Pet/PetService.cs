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
    
    public async Task<Pet> GetPet(long petId)
    {
        return await _petRepository.GetPet(petId);
    }
}