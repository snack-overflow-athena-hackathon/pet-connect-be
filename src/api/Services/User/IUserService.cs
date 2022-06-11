namespace pet;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
}