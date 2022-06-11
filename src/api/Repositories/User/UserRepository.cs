namespace pet;

public class UserRepository : IUserRepository
{
    public async Task<IEnumerable<User>> GetUsers()
    {
        var returnedUsers = new List<User>
        {
            new User
            {
                Id = 1,
                GenderId = 0,
                PetOwner = false,
                FirstName = "Sally",
                LastName = "Gunnell",
                PreferredName = "Sazza",
                Bio = "I am a cat lover, and I love to run.",
                PictureUrl = "https://pbs.twimg.com/profile_images/1419648918934233089/1OsddtPT_400x400.jpg",
                Email = "NotARealAddress@test.com"
            },
            new User
            {
                Id = 2,
                GenderId = 0,
                PetOwner = true,
                FirstName = "Taylor",
                LastName = "Swift",
                PreferredName = "Swiftinator",
                Bio = "Dogs are the best!",
                PictureUrl = "https://i1.sndcdn.com/avatars-000500544273-6kcyh0-t500x500.jpg",
                Email = "NotARealAddressEither@test.com"
            }
        };

        return returnedUsers;
    }
}