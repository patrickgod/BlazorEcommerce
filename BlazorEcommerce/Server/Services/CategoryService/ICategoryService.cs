namespace BlazorEcommerce.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Categories>>> GetCategories();
        Task<ServiceResponse<List<Categories>>> GetAdminCategories();
        Task<ServiceResponse<List<Categories>>> AddCategory(Categories category);
        Task<ServiceResponse<List<Categories>>> UpdateCategory(Categories category);
        Task<ServiceResponse<List<Categories>>> DeleteCategory(int id);
    }
}
