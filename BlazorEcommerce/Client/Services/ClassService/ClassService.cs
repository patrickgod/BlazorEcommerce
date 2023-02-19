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

        public List<ClassDto> Classes { get; set; } = new List<ClassDto>();
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

        public async Task<ClassDto> Create(ClassDto Class)
        {
            var result = await _http.PostAsJsonAsync("api/Class", Class);
            var newClass = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<ClassDto>>()).Data;
            return newClass;
        }

        public async Task Delete(ClassDto Class)
        {
            var result = await _http.DeleteAsync($"api/Class/{Class.Classid}");
        }

        public async Task<ClassDto> Update(ClassDto Class)
        {
            var result = await _http.PutAsJsonAsync($"api/Class", Class);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<ClassDto>>();
            return content.Data;
        }

        public async Task<List<ClassDto>> GetClassListExtended()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ClassDto>>>("api/Class");

            //if (result != null && result.Data != null)
                return result.Data;

       
        }

        public async Task GetClassList()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ClassDto>>>("api/Class");

            if (result != null && result.Data != null)
                Classes = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Classes.Count == 0)
                Message = "No products found";

            ClassChanged.Invoke();
        }

       
    }
}
