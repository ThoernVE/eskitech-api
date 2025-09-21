namespace EskitechApi.Services.ProductServices
{
    public interface IProductService
    {
        // Interface for abstraction of productservice
        List<Product> GetFullProductsExcel();

        List<string> GetProductNamesExcel();

        List<ProductPriceDTO> GetProductWithPricesExcel();

        List<ProductStockDTO> GetProductWithStockExcel();

        int GetProductCountExcel();

        Task<List<Product>> GetFullProductsDb();

        Task<List<string>> GetProductNamesDb();

        Task<List<ProductPriceDTO>>GetProductWithPricesDb();

        Task<List<ProductStockDTO>> GetProductWithStockDb();

        Task<int> GetProductCountDb();

        Task<PagedResult<Product>> GetProductsPaginated(int page, int pageSize);
    }
}