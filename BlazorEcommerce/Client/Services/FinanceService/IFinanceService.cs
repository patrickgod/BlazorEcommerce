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
        Task<FinanceDto> UpdateFinance(FinanceDto Finance, bool skipRecalculate = false);
        Task DeleteFinance(FinanceDto Finance);

        Task<bool> UpdateAllPersonDueDateOffset(int offsetDay);
    }
}
