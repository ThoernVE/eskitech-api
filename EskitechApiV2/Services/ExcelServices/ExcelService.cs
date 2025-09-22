using EskitechApi.Data;
using ExcelDataReader;


namespace EskitechApi.Services.ExcelServices
{
    public class ExcelService : IExcelService
    {
        private readonly EskitechDbContext _db;
        public ExcelService(EskitechDbContext db)
        {
            _db = db;
        }

        public async Task ReadToDatabase(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty.");


            var allowedExtensions = new[] { ".xls", ".xlsx" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException("Invalid file type. Only .xls and .xlsx files are allowed.");

            var existingProducts = _db.Products.Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);


            using (var stream = file.OpenReadStream())
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {


                    var products = new List<Product>();

                    do
                    {
                        bool isHeader = true;

                        while (reader.Read())
                        {

                            if (isHeader) { isHeader = false; continue; }

                            var name = reader.GetValue(0)?.ToString();
                            if (string.IsNullOrWhiteSpace(name)) continue;

                            if (existingProducts.Contains(name)) continue;

                            decimal.TryParse(reader.GetValue(1)?.ToString(), out var price);
                            int.TryParse(reader.GetValue(2)?.ToString(), out var stock);

                            var product = new Product
                            {
                                Name = name,
                                Price = price,
                                Stock = stock
                            };

                            products.Add(product);

                            existingProducts.Add(name);
                        }
                    } while (reader.NextResult());

                    await _db.Products.AddRangeAsync(products);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}