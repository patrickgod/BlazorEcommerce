using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Services.ClassService
{
    public interface IClassService
    {
         Task<ServiceResponse<List<ClassDto>>> GetClassListAsync();
         Task<ServiceResponse<ClassDto>> GetClassAsync(int ClassId);
         Task<ServiceResponse<ClassDto>> GetClassListByName(string searchText);
         Task<ServiceResponse<ClassDto>> CreateClass(ClassDto ClassDto);
         Task<ServiceResponse<ClassDto>> UpdateClass(ClassDto ClassDto);
        Task<ServiceResponse<List<IdValuePair>>> GetClassIdValuePair();
         Task<ServiceResponse<bool>> DeleteClass(Guid Id);

    }
}
