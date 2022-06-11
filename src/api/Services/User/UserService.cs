namespace pet;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPetRepository _petRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public UserService(IUserRepository userRepository, IPetRepository petRepository, IAppointmentRepository appointmentRepository)
    {
        _userRepository = userRepository;
        _petRepository = petRepository;
        _appointmentRepository = appointmentRepository;
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

    public async Task<User> GetUserByAppointmentId(long appointmentId)
    {
        var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);
        return await _userRepository.GetUserByUserId(appointment.OwnerId);
    }

    public async Task<long> AddUser(User user)
    {
        return await _userRepository.AddUser(user);
    }

    public async Task EditUser(User user)
    {
        await _userRepository.EditUser(user);
    }
}