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
        [Route("{userId}")]
        public async Task<ActionResult> GetPetsByUser(long userId)
        {
            return null;
        }
    }
}