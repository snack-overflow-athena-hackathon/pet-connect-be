namespace pet;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUserByUserId(long userId);
    Task<User> GetUserByPetId(long petId);
    Task<User> GetUserByAppointmentId(long appointmentId);
    Task<long> AddUser(User user);
    Task EditUser(User user);
}