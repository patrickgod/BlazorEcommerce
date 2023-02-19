using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Services.ClassService
{
    public class ClassService : IClassService
    {
        private readonly HttpClient _http;

        public ClassService(HttpClient http)
        {
            _http = http;
        }

        public List<ClassDto> Class { get; set; } = new List<ClassDto>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action ClassChanged;

        public async Task<List<IdValuePair>> GetClassIdValuePair()
        {

            var result = await _http.GetAsync("api/Class/GetClassIdValuePair");
            var resultData = (await result.Content.ReadFromJsonAsync<ServiceResponse<List<IdValuePair>>>()).Data;
            return resultData;
        }

        public async Task<ClassDto> CreateClass(ClassDto Class)
        {
            var result = await _http.PostAsJsonAsync("api/Class", Class);
            var newClass = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<ClassDto>>()).Data;
            return newClass;
        }

        public async Task DeleteProduct(ClassDto Class)
        {
            var result = await _http.DeleteAsync($"api/Class/{Class.Classid}");
        }

        public async Task<ClassDto> UpdateProduct(ClassDto Class)
        {
            var result = await _http.PutAsJsonAsync($"api/Class", Class);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<ClassDto>>();
            return content.Data;
        }


        public async Task GetClassList()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ClassDto>>>("api/Class");

            if (result != null && result.Data != null)
                Class = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Class.Count == 0)
                Message = "No products found";

            ClassChanged.Invoke();
        }

       
    }
}
