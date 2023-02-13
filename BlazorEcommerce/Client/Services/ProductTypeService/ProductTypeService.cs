namespace BlazorEcommerce.Client.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly HttpClient _http;

        public ProductTypeService(HttpClient http)
        {
            _http = http;
        }

        public List<ProductTypes> ProductTypes { get; set; } = new List<ProductTypes>();

        public event Action OnChange;

        public async Task AddProductType(ProductTypes productType)
        {
            var response = await _http.PostAsJsonAsync("api/producttype", productType);
            ProductTypes = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<ProductTypes>>>()).Data;
            OnChange.Invoke();
        }

        public ProductTypes CreateNewProductType()
        {
            var newProductType = new ProductTypes { IsNew = true, Editing = true };

            ProductTypes.Add(newProductType);
            OnChange.Invoke();
            return newProductType;
        }

        public async Task GetProductTypes()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<ProductTypes>>>("api/producttype");
            ProductTypes = result.Data;
        }

        public async Task UpdateProductType(ProductTypes productType)
        {
            var response = await _http.PutAsJsonAsync("api/producttype", productType);
            ProductTypes = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<ProductTypes>>>()).Data;
            OnChange.Invoke();
        }
    }
}
