using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Services.FinanceService
{
    public class FinanceService : IFinanceService
    {
        private readonly HttpClient _http;

        public FinanceService(HttpClient http)
        {
            _http = http;
        }

        public List<FinanceDto> Finance { get; set; } = new List<FinanceDto>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action FinanceChanged;

        public async Task<FinanceDto> CreateFinance(FinanceDto Finance)
        {
            var result = await _http.PostAsJsonAsync("api/Finance", Finance);
            var newFinance = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<FinanceDto>>()).Data;
            return newFinance;
        }

        
        public async Task DeleteFinance(FinanceDto Finance)
        {
            var result = await _http.DeleteAsync($"api/Finance/{Finance.Financeid}");
        }

        public async Task<FinanceDto> UpdateFinance(FinanceDto Finance, int selectedMonth)
        {
            var result = await _http.PutAsJsonAsync($"api/Finance/{selectedMonth}", Finance);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<FinanceDto>>();
            return content.Data;
        }


        public async Task GetFinanceList(int year, int month)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<FinanceDto>>>($"api/Finance/{year}/{month}");

            if (result != null && result.Data != null)
                Finance = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Finance.Count == 0)
                Message = "No products found";

            FinanceChanged.Invoke();
        }

    }
}
