using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Services.ClassService
{
    public interface IClassService
    {
        event Action ClassChanged;
        List<ClassDto> Class { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        
        Task GetClassList();
        //Task<ServiceResponse<Products>> GetProduct(int productId);
        //Task SearchProducts(string searchText, int page);
        //Task<List<string>> GetProductSearchSuggestions(string searchText);
        //Task GetAdminProducts();

        Task<ClassDto> CreateClass(ClassDto product);
        Task<ClassDto> UpdateProduct(ClassDto product);
        Task DeleteProduct(ClassDto product);

        Task<List<IdValuePair>> GetClassIdValuePair();
    }
}
