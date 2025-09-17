namespace EskitechApi.Services.ProductServices
{
 public class ProductService : IProductService
 {
  public ProductService()
  {

  }

  public List<string> GetProducts()
  {
   return new List<string> { "Product1", "Product2", "Product3" };
  }
  
  public int GetProductCount(string product)
  {
   string searchedProduct = product.ToLower();
   return GetProducts().Count;
  }
 }
}