namespace BlazorEcommerce.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange;
        List<Categories> Categories { get; set; }
        List<Categories> AdminCategories { get; set; }
        Task GetCategories();
        Task GetAdminCategories();
        Task AddCategory(Categories category);
        Task UpdateCategory(Categories category);
        Task DeleteCategory(int categoryId);
        Categories CreateNewCategory();
    }
}
