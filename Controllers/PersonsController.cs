using Business.Contracts;
using Business.Providers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PersonsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonProvider _personProvider;

        public PersonsController(IPersonProvider personProvider)
        {
            _personProvider = personProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            return Ok(_personProvider.GetAllPersons());
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPersonById(int personId)
        {
            var person = _personProvider.GetPersonById(personId);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            var response = _personProvider.SavePerson(person);
            if (!response.IsSuccessful)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            var response = _personProvider.SavePerson(person);
            if (!response.IsSuccessful)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int personId)
        {
            var response = _personProvider.DeletePerson(personId);
            if (!response.IsSuccessful)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
