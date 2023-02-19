using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Client.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;

        public PersonService(HttpClient http)
        {
            _http = http;
        }

        public List<PersonDto> Person { get; set; } = new List<PersonDto>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action PersonChanged;

        public async Task<PersonDto> CreatePerson(PersonDto person)
        {
            var result = await _http.PostAsJsonAsync("api/Person", person);
            var newPerson = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<PersonDto>>()).Data;
            return newPerson;
        }

        public async Task DeleteProduct(PersonDto person)
        {
            var result = await _http.DeleteAsync($"api/Person/{person.Personid}");
        }

        public async Task<PersonDto> UpdateProduct(PersonDto person)
        {
            var result = await _http.PutAsJsonAsync($"api/Person", person);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<PersonDto>>();
            return content.Data;
        }


        public async Task GetPersonList()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<PersonDto>>>("api/Person");

            if (result != null && result.Data != null)
                Person = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Person.Count == 0)
                Message = "No products found";

            PersonChanged.Invoke();
        }

    }
}
