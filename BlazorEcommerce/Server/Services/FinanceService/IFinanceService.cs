using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Services.FinanceService
{
    public interface IFinanceService
    {
         Task<ServiceResponse<List<FinanceDto>>> GetFinanceListAsync();
        Task<ServiceResponse<List<FinanceDto>>> GetFinanceListAsync(int day, int month);
         Task<ServiceResponse<FinanceDto>> GetFinanceAsync(int FinanceId);
         Task<ServiceResponse<FinanceDto>> GetFinanceListByName(string searchText);
         Task<ServiceResponse<FinanceDto>> CreateFinance(FinanceDto FinanceDto);
         Task<ServiceResponse<FinanceDto>> UpdateFinance(FinanceDto FinanceDto,int selectedMonth);
         Task<ServiceResponse<bool>> DeleteFinance(Guid FinanceDtoId);
        Task DeleteByPersonId(Guid personId);
        Task FillData();

    }
}
