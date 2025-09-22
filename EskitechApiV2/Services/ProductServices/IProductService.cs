namespace EskitechApi.Services.ProductServices
{
    public interface IProductService
    {
        // Interface for abstraction of productservice

        Task<List<Product>> GetFullProductsDb();

        Task<List<string>> GetProductNamesDb();

        Task<List<ProductPriceDTO>>GetProductWithPricesDb();

        Task<List<ProductStockDTO>> GetProductWithStockDb();

        Task<int> GetProductCountDb();

        Task<PagedResult<Product>> GetProductsPaginated(int page, int pageSize);
    }
}