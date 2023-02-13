using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ProductTypes>>>> GetProductTypes()
        {
            var response = await _productTypeService.GetProductTypes();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<ProductTypes>>>> AddProductType(ProductTypes productType)
        {
            var response = await _productTypeService.AddProductType(productType);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<ProductTypes>>>> UpdateProductType(ProductTypes productType)
        {
            var response = await _productTypeService.UpdateProductType(productType);
            return Ok(response);
        }
    }
}
