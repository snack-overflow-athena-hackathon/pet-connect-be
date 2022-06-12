namespace pet;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;

    private const string DefaultPetIcon =
        "https://icons-for-free.com/iconfiles/png/512/for+pets-1320567788453799684.png";

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

    public async Task<IEnumerable<Pet>> GetPetsByUserId(long userId)
    {
        return await _petRepository.GetPetsByUserId(userId);
    }

    public async Task<long> AddPet(Pet pet)
    {
        return await _petRepository.AddPet(Cleansed(pet));
    }

    private static Pet Cleansed(Pet pet)
    {
        if (string.IsNullOrEmpty(pet.PictureUrl) || !IsValidUri(pet.PictureUrl))
        {
            pet.PictureUrl = DefaultPetIcon;
        }
        return pet;
    }

    public static bool IsValidUri(string uri)
    {
        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            return false;
        if (!Uri.TryCreate(uri, UriKind.Absolute, out var tmp))
            return false;
        return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
    }

    public async Task EditPet(Pet pet)
    {
        await _petRepository.EditPet(pet);
    }
}