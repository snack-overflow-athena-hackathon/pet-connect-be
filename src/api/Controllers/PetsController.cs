using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IUserService _userService;

        public PetsController(IPetService petService, IUserService userService)
        {
            _petService = petService;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(Pet), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("{petId}")]
        public async Task<ActionResult> GetPetById(long petId)
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

        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("{petId}/User")]
        public async Task<ActionResult> GetUserByPetId(long petId)
        {
            try
            {
                var returnedUser = await _userService.GetUserByPetId(petId);
                return Ok(returnedUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Pet), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPet([FromBody] Pet pet)
        {
            try
            {
                var id = await _petService.AddPet(pet);
                pet.Id = id;

                return Created($"{id}", pet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditPet([FromBody] Pet pet)
        {
            try
            {
                await _petService.EditPet(pet);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}