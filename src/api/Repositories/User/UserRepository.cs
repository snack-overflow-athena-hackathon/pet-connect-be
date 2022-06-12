using pet.Repositories;
using Serilog;

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
        var users = await _query.QueryAsync<User>(sqlStatement);

        return users.Select(MapDisplayName);
    }

    public async Task<User> GetUserByUserId(long userId)
    {
        var sql = GetUserSqlStatement();
        var user = await _query.QueryFirstOrDefaultAsync<User>(sql, new
        {
            UserId = userId
        });

        return MapDisplayName(user);
    }

    private static User MapDisplayName(User user)
    {
        var pronoun = string.IsNullOrEmpty(user.Pronouns) ? null : " (" + user.Pronouns + ")";
        user.DisplayName = (string.IsNullOrEmpty(user.PreferredName) ? user.FirstName + " " + user.LastName : user.PreferredName) +
                           (pronoun ?? "");
        return user;
    }

    public async Task<long> AddUser(User user)
    {
        var sql = AddUserSqlStatement();
        var id = await _query.ExecuteScalarAsync<long>(sql, user);
        
        Log.Information($"Added new user {user.FirstName} {user.LastName} to database with id {id}");

        return id;
    }

    public async Task EditUser(User user)
    {
        var sql = EditUserSqlStatement();
        await _query.ExecuteAsync(sql, user);
        
        Log.Information($"Edited user {user.FirstName} {user.LastName}.");
    }

    private static string GetAllUsersSqlStatement()
    {
        return $@"SELECT Id, PetOwner, Gender, FirstName, LastName, Pronouns, PreferredName, Email, Bio, PictureUrl, Deactivated FROM Users";
    }

    private static string GetUserSqlStatement()
    {
        return $@"
            SELECT Id, PetOwner, Gender, FirstName, LastName, Pronouns, PreferredName, Email, Bio, PictureUrl, Deactivated
            FROM Users 
            WHERE Id = @UserId";
    }
    
    private static string AddUserSqlStatement()
    {
        return $@"INSERT INTO Users
                 (
                   PetOwner, Gender, FirstName, LastName, Pronouns, PreferredName, Email, Bio, PictureUrl
                 )
                 VALUES
                 (
                   @PetOwner, @Gender, @FirstName, @LastName, @Pronouns, @PreferredName, @Email, @Bio, @PictureUrl
                 ) RETURNING Id";
    }
    
    private static string EditUserSqlStatement()
    {
        return $@"UPDATE Users
                    SET
                    PetOwner = @PetOwner,
                    Gender = @Gender ,
                    FirstName = @FirstName,
                    LastName = @LastName ,
                    Pronouns = @Pronouns,
                    PreferredName = @PreferredName,
                    Email = @Email,
                    Bio = @Bio,
                    PictureUrl = @PictureUrl,
                    Deactivated = @Deactivated
                    WHERE Id = @Id";
    }
}