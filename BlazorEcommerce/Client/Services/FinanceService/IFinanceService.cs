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

        Task GetFinanceList(int year, int month);

        Task<FinanceDto> CreateFinance(FinanceDto Finance);
        Task<FinanceDto> UpdateFinance(FinanceDto Finance, int selectedMonth);
        Task DeleteFinance(FinanceDto Finance);
    }
}
