namespace pet;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUserByUserId(long userId);
}