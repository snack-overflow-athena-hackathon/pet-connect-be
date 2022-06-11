using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PetsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPets()
        {
            var returnedPets = new List<Pet>
            {
                new(id: 123,
                    ownerId: 456, typeId: 1, breed: "Doberman", name: "Max",
                    bio: "Max is nice.",
                    pictureUrl:
                    "https://www.akc.org/wp-content/uploads/2017/11/Doberman-Pinscher-standing-outdoors.jpg",
                    listOrder: 0),
                new(id: 124,
                    ownerId: 456, typeId: 1, breed: "Bischon Frise", name: "Middy",
                    bio: "Middy is not nice.",
                    pictureUrl:
                    "https://dogtime.com/assets/uploads/gallery/bichon-frise-dog-breed-pictures/1-sitting.jpg",
                    listOrder: 1)
            };

            return Ok(returnedPets);
        }
    }

    public class Pet
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public long TypeId { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string PictureUrl { get; set; }
        public int ListOrder { get; set; }

        public Pet(long id, long ownerId, long typeId, string breed, string name, string bio, string pictureUrl, int listOrder)
        {
            Id = id;
            OwnerId = ownerId;
            TypeId = typeId;
            Breed = breed;
            Name = name;
            Bio = bio;
            PictureUrl = pictureUrl;
            ListOrder = listOrder;
        }
    }
}