namespace BlazorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Products> Products { get; set; }
        List<Products> AdminProducts { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Products>> GetProduct(int productId);
        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        Task GetAdminProducts();
        Task<Products> CreateProduct(Products product);
        Task<Products> UpdateProduct(Products product);
        Task DeleteProduct(Products product);
    }
}
