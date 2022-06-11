namespace pet;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
}