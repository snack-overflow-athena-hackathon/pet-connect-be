namespace pet;

public class PetRepository : IPetRepository
{
    public async Task<IEnumerable<Pet>> GetPets()
    {
        var returnedPets = new List<Pet>
        {
            new Pet
            {
                Id = 123,
                OwnerId = 456,
                TypeId = 1,
                Breed = "Doberman",
                Name = "Max",
                Bio = "Max is nice.",
                PictureUrl = "https://www.akc.org/wp-content/uploads/2017/11/Doberman-Pinscher-standing-outdoors.jpg",
                ListOrder = 0
            },
            new Pet
            {
                Id = 124,
                OwnerId = 456,
                TypeId = 1,
                Breed = "Bischon Frise",
                Name = "Middy",
                Bio = "Middy is not nice.",
                PictureUrl = "https://dogtime.com/assets/uploads/gallery/bichon-frise-dog-breed-pictures/1-sitting.jpg",
                ListOrder = 1
            }
        };

        return returnedPets;
    }
}