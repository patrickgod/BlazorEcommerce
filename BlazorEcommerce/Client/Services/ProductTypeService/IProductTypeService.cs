namespace BlazorEcommerce.Client.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        event Action OnChange;
        public List<ProductTypes> ProductTypes { get; set; }
        Task GetProductTypes();
        Task AddProductType(ProductTypes productType);
        Task UpdateProductType(ProductTypes productType);
        ProductTypes CreateNewProductType();
    }
}
