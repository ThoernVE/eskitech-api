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

        List<Product> GetFullProductsDb();

        List<string> GetProductNamesDb();

        List<ProductPriceDTO> GetProductWithPricesDb();

        List<ProductStockDTO> GetProductWithStockDb();

        int GetProductCountDb();

        List<Product> GetProductsPaginated(int page, int pageSize);
    }
}