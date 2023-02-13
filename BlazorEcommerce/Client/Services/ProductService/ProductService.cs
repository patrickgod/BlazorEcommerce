namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public List<Products> Products { get; set; } = new List<Products>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;
        public List<Products> AdminProducts { get; set; }

        public event Action ProductsChanged;

        public async Task<Products> CreateProduct(Products product)
        {
            var result = await _http.PostAsJsonAsync("api/product", product);
            var newProduct = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Products>>()).Data;
            return newProduct;
        }

        public async Task DeleteProduct(Products product)
        {
            var result = await _http.DeleteAsync($"api/product/{product.Id}");
        }

        public async Task GetAdminProducts()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<Products>>>("api/product/admin");
            AdminProducts = result.Data;
            CurrentPage = 1;
            PageCount = 0;
            if (AdminProducts.Count == 0)
                Message = "No products found.";
        }

        public async Task<ServiceResponse<Products>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Products>>($"api/product/{productId}");
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Products>>>("api/product/featured") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Products>>>($"api/product/category/{categoryUrl}");
            if (result != null && result.Data != null)
                Products = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
                Message = "No products found";

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
            return result.Data;
        }

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{searchText}/{page}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0) Message = "No products found.";
            ProductsChanged?.Invoke();
        }

        public async Task<Products> UpdateProduct(Products product)
        {
            var result = await _http.PutAsJsonAsync($"api/product", product);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Products>>();
            return content.Data;
        }
    }
}
