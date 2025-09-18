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
        
    }
}