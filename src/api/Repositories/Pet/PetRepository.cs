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
    
    public async Task<IEnumerable<Pet>> GetPetsByUserId(long ownerId)
    {
        var sql = GetPetsByOwnerIdSqlStatement();
        var pets = await _query.QueryAsync<Pet>(sql, new
        {
            OwnerId = ownerId
        });
        
        Log.Information($"Getting all pets for owner Id {ownerId}, found {pets.Count()} in database for this user.");
        
        return pets;
    }
    
    public async Task<Pet> GetPetByPetId(long petId)
    {
        var sql = GetPetSqlStatement();
        var returnedPet = await _query.QueryFirstOrDefaultAsync<Pet>(sql, new
        {
            Id = petId
        });

        return returnedPet;
    }

    public async Task<long> AddPet(Pet pet)
    {
        var sql = AddPetSqlStatement();
        var id = await _query.ExecuteScalarAsync<long>(sql, pet);
        
        Log.Information($"Added new pet {pet.PetName} to database with id {id}");

        return id;
    }
    
    public async Task EditPet(Pet pet)
    {
        var sql = EditPetSqlStatement();
        await _query.ExecuteAsync(sql, pet);
        
        Log.Information($"Edited Pet {pet.PetName}.");
    }

    private static string GetAllPetsSqlStatement()
    {
        return $@"
                SELECT Pets.Id, Pets.OwnerId, Pets.TypeId, Pets.Breed, Pets.PetName, Pets.Gender, Pets.Bio, Pets.PictureUrl, Pets.ListOrder, Pets.Deactivated,
                type.Animal
                FROM Pets
                LEFT JOIN AnimalType type ON type.Id = Pets.TypeId";
    }
    private static string AddPetSqlStatement()
    {
        return $@"INSERT INTO Pets
                 (
                   OwnerId, TypeId, Breed, PetName, Gender, Bio, PictureUrl, ListOrder, Deactivated
                 )
                 VALUES
                 (
                   @OwnerId, @TypeId, @Breed, @PetName, @Gender, @Bio, @PictureUrl, @ListOrder, @Deactivated
                 ) RETURNING Id";
    }
    
    private static string EditPetSqlStatement()
    {
        return $@"UPDATE Pets
                    SET
                    OwnerId = @OwnerId, 
                    TypeId = @TypeId, 
                    Breed = @Breed, 
                    PetName = @PetName, 
                    Gender = @Gender, 
                    Bio = @Bio, 
                    PictureUrl = @PictureUrl, 
                    ListOrder = @ListOrder,
                    Deactivated = @Deactivated
                    WHERE Id = @Id";
    }
    
    private static string GetPetSqlStatement()
    {
        return $@"
                SELECT Id, OwnerId, TypeId, Breed, PetName, Gender, Bio, PictureUrl, ListOrder, Deactivated
                FROM Pets
                WHERE Id = @Id";
    }
    
    private static string GetPetsByOwnerIdSqlStatement()
    {
        return $@"
                SELECT Id, OwnerId, TypeId, Breed, PetName, Gender, Bio, PictureUrl, ListOrder, Deactivated
                FROM Pets
                WHERE OwnerId = @OwnerId";
    }
}