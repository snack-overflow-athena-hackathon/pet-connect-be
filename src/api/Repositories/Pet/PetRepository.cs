using pet.Repositories;

namespace pet;

public class PetRepository : IPetRepository
{
    private IQuery _query;

    public PetRepository(IQuery query)
    {
        _query = query;
    }

    public async Task<IEnumerable<Pet>> GetPets()
    {
        var sqlStatement = GetAllPetsSqlStatement();
        return await _query.QueryAsync<Pet>(sqlStatement);
    }

    private static string GetAllPetsSqlStatement()
    {
        return $@"SELECT Id, OwnerId, TypeId, Breed, PetName, Gender, Bio, PictureUrl, ListOrder FROM Pets";
    }


    public async Task<Pet> GetPet(long petId)
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
}