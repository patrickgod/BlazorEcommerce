namespace BlazorEcommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<Categories> Categories { get; set; } = new List<Categories>();
        public List<Categories> AdminCategories { get; set; } = new List<Categories>();

        public event Action OnChange;

        public async Task AddCategory(Categories category)
        {
            var response = await _http.PostAsJsonAsync("api/Category/admin", category);
            var result  =  await response?.Content?.ReadFromJsonAsync<ServiceResponse<List<Categories>>>();
            AdminCategories = result.Data;
            await GetCategories();
            OnChange.Invoke();
        }

        public Categories CreateNewCategory()
        {
            var newCategory = new Categories { IsNew = true, Editing = true };
            AdminCategories.Add(newCategory);
            OnChange.Invoke();
            return newCategory;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var response = await _http.DeleteAsync($"api/Category/admin/{categoryId}");
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Categories>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }

        public async Task GetAdminCategories()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<Categories>>>("api/Category/admin");
                if (response != null && response.Data != null)
                    AdminCategories = response.Data;
            }
            catch (Exception exp)
            {

                throw;
            }
            
        }

        public async Task GetCategories()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<Categories>>>("api/Category");
                if (response != null && response.Data != null)
                    Categories = response.Data;
            }
            catch (Exception exp)
            {

                throw;
            }
           
        }

        public async Task UpdateCategory(Categories category)
        {
            var response = await _http.PutAsJsonAsync("api/Category/admin", category);
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Categories>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }
    }
}
