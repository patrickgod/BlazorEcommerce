using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Services.PersonService
{
    public interface IPersonService
    {
        event Action PersonChanged;
        List<PersonDto> Person { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        
        Task GetPersonList();
        //Task<ServiceResponse<Products>> GetProduct(int productId);
        //Task SearchProducts(string searchText, int page);
        //Task<List<string>> GetProductSearchSuggestions(string searchText);
        //Task GetAdminProducts();

        Task<PersonDto> CreatePerson(PersonDto product);
        Task<PersonDto> UpdateProduct(PersonDto product);
        Task DeleteProduct(PersonDto product);
    }
}
