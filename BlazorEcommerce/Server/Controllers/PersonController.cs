using BlazorEcommerce.Server.Services;
using BlazorEcommerce.Server.Services.PersonService;
using BlazorEcommerce.Shared.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<PersonDto>>> CreatePerson(PersonDto person)
        {
            var result = await _personService.CreatePerson(person);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<PersonDto>>> UpdatePerson(PersonDto person)
        {
            var result = await _personService.UpdatePerson(person);
            return Ok(result);
        }

        [HttpDelete("{Personid}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeletePerson(Guid Personid)
        {
            var result = await _personService.DeletePerson(Personid);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<PersonDto>>>> GetPersonList()
        {
            var result = await _personService.GetPersonListAsync();
            return Ok(result);
        }

        [HttpGet("{PersonId}")]
        public async Task<ActionResult<ServiceResponse<PersonDto>>> GetPerson(int PersonId)
        {
            var result = await _personService.GetPersonAsync(PersonId);
            return Ok(result);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<ServiceResponse<List<PersonDto>>>> GetPersonListByName(string name)
        {
            var result = await _personService.GetPersonListByName(name);
            return Ok(result);
        }


    }


}
