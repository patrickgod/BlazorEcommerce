namespace BlazorEcommerce.Server.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        Task<ServiceResponse<List<ProductTypes>>> GetProductTypes();
        Task<ServiceResponse<List<ProductTypes>>> AddProductType(ProductTypes productType);
        Task<ServiceResponse<List<ProductTypes>>> UpdateProductType(ProductTypes productType);


    }
}
