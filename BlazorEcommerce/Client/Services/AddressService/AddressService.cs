namespace BlazorEcommerce.Client.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _http;

        public AddressService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Addresses> AddOrUpdateAddress(Addresses address)
        {
            var response = await _http.PostAsJsonAsync("api/address", address);
            return response.Content
                .ReadFromJsonAsync<ServiceResponse<Addresses>>().Result.Data;
        }

        public async Task<Addresses> GetAddress()
        {
            var response = await _http
                .GetFromJsonAsync<ServiceResponse<Addresses>>("api/address");
            return response.Data;
        }
    }
}
