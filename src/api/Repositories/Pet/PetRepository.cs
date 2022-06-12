using pet.Models.DBEntities;
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
        var pets = await _query.QueryAsync<PetDbEntity>(sql);
        
        Log.Information($"Getting all pets, found {pets.Count()} in database");

        return pets.Select(MapToContract);
    }

    public async Task<IEnumerable<Pet>> GetPetsByUserId(long ownerId)
    {
        var sql = GetPetsByOwnerIdSqlStatement();
        var pets = await _query.QueryAsync<PetDbEntity>(sql, new
        {
            OwnerId = ownerId
        });
        
        Log.Information($"Getting all pets for owner Id {ownerId}, found {pets.Count()} in database for this user.");

        return pets.Select(MapToContract);
    }
    
    public async Task<Pet> GetPetByPetId(long petId)
    {
        var sql = GetPetSqlStatement();
        var pet = await _query.QueryFirstOrDefaultAsync<PetDbEntity>(sql, new
        {
            Id = petId
        });

        return MapToContract(pet);
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

    private static Pet MapToContract(PetDbEntity petDbEntity)
    {
        return new Pet()
        {
            Id = petDbEntity.Id,
            Breed = petDbEntity.Breed,
            PetName = petDbEntity.PetName,
            OwnerId = petDbEntity.OwnerId,
            TypeId = petDbEntity.TypeId,
            Gender = petDbEntity.Gender,
            Animal = petDbEntity.Animal,
            PictureUrl = petDbEntity.PictureUrl,
            Bio = petDbEntity.Bio,
            Deactivated = petDbEntity.Deactivated,
            ListOrder = petDbEntity.ListOrder,
            OwnerName = petDbEntity.FirstName + " " + petDbEntity.LastName
        };
    }

    private static string GetAllPetsSqlStatement()
    {
        return @"
                 SELECT p.Id, p.OwnerId, p.TypeId, p.Breed, p.PetName, p.Gender, p.Bio, p.PictureUrl, p.ListOrder, p.Deactivated, a.Animal, u.FirstName, u.LastName 
                 FROM Pets p
                 LEFT JOIN AnimalType a ON a.Id = p.TypeId
                 LEFT JOIN Users u ON u.Id = p.OwnerId";
    }

    private static string GetPetSqlStatement()
    {
        return @"
                 SELECT p.Id, p.OwnerId, p.TypeId, p.Breed, p.PetName, p.Gender, p.Bio, p.PictureUrl, p.ListOrder, p.Deactivated, a.Animal, u.FirstName, u.LastName 
                 FROM Pets p
                 LEFT JOIN AnimalType a ON a.Id = p.TypeId
                 LEFT JOIN Users u ON u.Id = p.OwnerId
                 WHERE Id = @Id";
    }
    
    private static string GetPetsByOwnerIdSqlStatement()
    {
        return @"
                 SELECT p.Id, p.OwnerId, p.TypeId, p.Breed, p.PetName, p.Gender, p.Bio, p.PictureUrl, p.ListOrder, p.Deactivated, a.Animal, u.FirstName, u.LastName 
                 FROM Pets p
                 LEFT JOIN AnimalType a ON a.Id = p.TypeId
                 LEFT JOIN Users u ON u.Id = p.OwnerId
                 WHERE OwnerId = @OwnerId";
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

}