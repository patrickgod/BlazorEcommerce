using BlazorEcommerce.Server.Services.ClassService;
using BlazorEcommerce.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassController : Controller
    {
        private readonly IClassService _ClassService;

        
        public ClassController(IClassService ClassService)
        {
            _ClassService = ClassService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ClassDto>>> CreateClass(ClassDto Class)
        {
            var result = await _ClassService.CreateClass(Class);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ClassDto>>> UpdateClass(ClassDto Class)
        {
            var result = await _ClassService.UpdateClass(Class);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteClass(int id)
        {
            var result = await _ClassService.DeleteClass(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ClassDto>>>> GetClassList()
        {
            var result = await _ClassService.GetClassListAsync();
            return Ok(result);
        }

        [HttpGet("GetClassIdValuePair")]
        public async Task<ActionResult<ServiceResponse<List<IdValuePair>>>> GetClassIdValuePair()
        {
            var result = await _ClassService.GetClassIdValuePair();
            return Ok(result);
        }

        [HttpGet("{ClassId}")]
        public async Task<ActionResult<ServiceResponse<ClassDto>>> GetClass(int ClassId)
        {
            var result = await _ClassService.GetClassAsync(ClassId);
            return Ok(result);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<ServiceResponse<List<ClassDto>>>> GetClassListByName(string name)
        {
            var result = await _ClassService.GetClassListByName(name);
            return Ok(result);
        }


    }


}
