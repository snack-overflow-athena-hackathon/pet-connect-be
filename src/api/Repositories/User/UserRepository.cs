using pet.Repositories;

namespace pet;

public class UserRepository : IUserRepository
{
    private IQuery _query;

    public UserRepository(IQuery query)
    {
        _query = query;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var sqlStatement = GetAllUsersSqlStatement();
        return await _query.QueryAsync<User>(sqlStatement);
    }

    public async Task<User> GetUserByUserId(long userId)
    {
        var sqlStatement = $@"
            SELECT Id, PetOwner, Gender, FirstName, LastName, Pronouns, PreferredName, Email, Bio, PictureUrl 
            FROM Users 
            WHERE Id = @UserId";
        
        var returnedUser = await _query.QueryFirstOrDefaultAsync<User>(sqlStatement, new
        {
            UserId = userId
        });

        return returnedUser;
    }

    private static string GetAllUsersSqlStatement()
    {
        return $@"SELECT Id, PetOwner, Gender, FirstName, LastName, Pronouns, PreferredName, Email, Bio, PictureUrl FROM Users";
    }
}