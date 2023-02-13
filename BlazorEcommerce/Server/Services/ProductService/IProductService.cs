namespace BlazorEcommerce.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Products>>> GetProductsAsync();
        Task<ServiceResponse<Products>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Products>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task<ServiceResponse<List<Products>>> GetFeaturedProducts();
        Task<ServiceResponse<List<Products>>> GetAdminProducts();
        Task<ServiceResponse<Products>> CreateProduct(Products product);
        Task<ServiceResponse<Products>> UpdateProduct(Products product);
        Task<ServiceResponse<bool>> DeleteProduct(int productId);

    }
}
