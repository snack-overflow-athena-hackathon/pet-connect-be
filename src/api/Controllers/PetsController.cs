using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using pet;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPets()
        {
            var returnedPets = await _petService.GetPets();

            return Ok(returnedPets);
        }

        [HttpGet]
        [Route("/ByUserId/{userId}")]
        public async Task<ActionResult> GetPetsByUserId(long userId)
        {
            return null;
        }
        
        [HttpGet]
        [Route("/ByPetId/{petId}")]
        public async Task<ActionResult> GetPetByPetId(long petId)
        {
            var returnedPet = await _petService.GetPet(petId);

            return Ok(returnedPet);
        }
        
        
    }
}