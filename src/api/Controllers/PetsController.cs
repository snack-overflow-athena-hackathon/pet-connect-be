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
            try
            {
                var returnedPets = await _petService.GetPets();
                return Ok(returnedPets);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("ByUserId/{userId}")]
        public async Task<ActionResult> GetPetsByUserId(long userId)
        {
            try
            {
                var returnedPet = await _petService.GetPetByUserId(userId);
                return Ok(returnedPet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
        
        [HttpGet]
        [Route("ByPetId/{petId}")]
        public async Task<ActionResult> GetPetByPetId(long petId)
        {
            try
            {
                var returnedPet = await _petService.GetPetByPetId(petId);
                return Ok(returnedPet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
        
        
    }
}