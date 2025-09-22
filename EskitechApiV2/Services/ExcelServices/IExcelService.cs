namespace EskitechApi.Services.ExcelServices
{
    public interface IExcelService
    {
        Task ReadToDatabase(IFormFile file);
    }
}