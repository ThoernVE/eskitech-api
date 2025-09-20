namespace EskitechApi.Services.ExcelServices
{
    public interface IExcelService
    {
        List<Product> GetProductsFromExcel();
        Task ReadToDatabase(IFormFile file);
    }
}