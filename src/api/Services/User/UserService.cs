namespace pet;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPetRepository _petRepository;

    public UserService(IUserRepository userRepository, 
        IPetRepository petRepository)
    {
        _userRepository = userRepository;
        _petRepository = petRepository;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _userRepository.GetUsers();
    }

    public async Task<User> GetUserByUserId(long userId)
    {
        return await _userRepository.GetUserByUserId(userId);
    }

    public async Task<User> GetUserByPetId(long petId)
    {
        var petData = await _petRepository.GetPetByPetId(petId);
        return await _userRepository.GetUserByUserId(petData.OwnerId);
    }
}