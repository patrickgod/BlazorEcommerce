namespace BlazorEcommerce.Client.Services.AddressService
{
    public interface IAddressService
    {
        Task<Addresses> GetAddress();
        Task<Addresses> AddOrUpdateAddress(Addresses address);
    }
}
