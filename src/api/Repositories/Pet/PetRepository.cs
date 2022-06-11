using pet.Repositories;
using Serilog;

namespace pet;

public class PetRepository : IPetRepository
{
    private readonly IQuery _query;

    public PetRepository(IQuery query)
    {
        _query = query;
    }

    public async Task<IEnumerable<Pet>> GetPets()
    {
        var sql = GetAllPetsSqlStatement();
        var pets = await _query.QueryAsync<Pet>(sql);
        
        Log.Information($"Getting all pets, found {pets.Count()} in database");
        
        return pets;
    }

    public async Task<long> AddPet(Pet pet)
    {
        var sql = AddPetSqlStatement();
        var id = await _query.ExecuteScalarAsync<long>(sql, pet);
        
        Log.Information($"Added new pet {pet.PetName} to database with id {id}");

        return id;
    }

    private static string GetAllPetsSqlStatement()
    {
        return $@"SELECT Id, OwnerId, TypeId, Breed, PetName, Gender, Bio, PictureUrl, ListOrder FROM Pets";
    }
    private static string AddPetSqlStatement()
    {
        return $@"INSERT INTO Pets
                 (
                   OwnerId, TypeId, Breed, PetName, Gender, Bio, PictureUrl, ListOrder
                 )
                 VALUES
                 (
                   @OwnerId, @TypeId, @Breed, @PetName, @Gender, @Bio, @PictureUrl, @ListOrder
                 ) RETURNING Id";
    }

    public async Task<Pet> GetPetByPetId(long petId)
    {
        return new Pet
        {
            Id = petId,
            OwnerId = 456,
            TypeId = 1,
            Breed = "Doberman",
            PetName = "Max",
            Bio = "Max is nice.",
            PictureUrl = "https://www.akc.org/wp-content/uploads/2017/11/Doberman-Pinscher-standing-outdoors.jpg",
            ListOrder = 0
        };
    }

    public async Task<IEnumerable<Pet>> GetPetsByUserId(long userId)
    {
        return new List<Pet>
        {
            new Pet
            {
                Id = 11,
                OwnerId = userId,
                TypeId = 1,
                Breed = "Doberman",
                PetName = "Max",
                Bio = "Max is nice.",
                PictureUrl = "https://www.akc.org/wp-content/uploads/2017/11/Doberman-Pinscher-standing-outdoors.jpg",
                ListOrder = 0
            },
            new Pet
            {
                Id = 12,
                OwnerId = userId,
                TypeId = 1,
                Breed = "Another Doberman",
                PetName = "Max 2",
                Bio = "Other Max is not so nice.",
                PictureUrl = "https://www.akc.org/wp-content/uploads/2017/11/Doberman-Pinscher-standing-outdoors.jpg",
                ListOrder = 1
            }
        };
    }
}