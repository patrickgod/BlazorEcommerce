using BlazorEcommerce.Server.Services.FinanceService;
using BlazorEcommerce.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinanceController : Controller
    {
        private readonly IFinanceService _FinanceService;

        
        public FinanceController(IFinanceService FinanceService)
        {
            _FinanceService = FinanceService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FinanceDto>>> CreateFinance(FinanceDto Finance)
        {
            var result = await _FinanceService.CreateFinance(Finance);
            return Ok(result);
        }

        [HttpPut("{skipRecalculate}")]
        public async Task<ActionResult<ServiceResponse<FinanceDto>>> UpdateFinance(FinanceDto Finance, [FromRoute] bool skipRecalculate = false)
        {
            var result = await _FinanceService.UpdateFinance(Finance, skipRecalculate);
            return Ok(result);
        }


        [HttpPut("offsetDay")]
        public async Task<ActionResult<bool>> UpdateFinance([FromBody]  int offsetDay)
        {
            //var result = await _FinanceService.UpdateFinance(Finance, selectedMonth);
            return Ok(true);
        }

        [HttpDelete("{Financeid}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteFinance(Guid Financeid)
        {
            var result = await _FinanceService.DeleteFinance(Financeid);
            return Ok(result);
        }

        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<ServiceResponse<List<FinanceDto>>>> GetFinanceList([FromRoute]int year, [FromRoute] int month)
        {
            var result = await _FinanceService.GetFinanceListAsync(year, month);
            return Ok(result);
        }

        [HttpGet("{FinanceId}")]
        public async Task<ActionResult<ServiceResponse<FinanceDto>>> GetFinance(int FinanceId)
        {
            var result = await _FinanceService.GetFinanceAsync(FinanceId);
            return Ok(result);
        }


        [HttpGet("FillData")]
        public async Task FillData()
        {
            await _FinanceService.FillData();
            //return Ok(result);
        }

    }


}
