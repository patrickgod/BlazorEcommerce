using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Services.FinanceService
{
    public interface IFinanceService
    {
        event Action FinanceChanged;
        List<FinanceDto> Finance { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        
        Task GetFinanceList(int day, int month);
        //Task<ServiceResponse<Finances>> GetFinance(int FinanceId);
        //Task SearchFinances(string searchText, int page);
        //Task<List<string>> GetFinanceSearchSuggestions(string searchText);
        //Task GetAdminFinances();

        Task<FinanceDto> CreateFinance(FinanceDto Finance);
        Task<FinanceDto> UpdateFinance(FinanceDto Finance, int selectedMonth);
        Task DeleteFinance(FinanceDto Finance);
    }
}
