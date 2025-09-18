using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;


namespace EskitechApi.Services.ExcelServices
{
    public class ExcelService : IExcelService
    {
        public ExcelService()
        {
        }

        public List<Product> GetProductsFromExcel()
        {
            var products = new List<Product>();

            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "skistore_produkter.xlsx");
            Console.WriteLine($"Loading products from: {filePath}");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Excel file not found.", filePath);
            }

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    
                    int productId = 1;
                    bool isHeader = true;

                    while (reader.Read())
                    {
                        if (isHeader) { isHeader = false; continue;}

                        var product = new Product
                        {
                            Id = productId++,
                            Name = reader.GetValue(0)?.ToString(),
                            Price = Convert.ToDecimal(reader.GetValue(1)),
                            Stock = Convert.ToInt32(reader.GetValue(2))
                        };

                        products.Add(product);
                        
                        }
                }
            }

            return products;
        }
    }
}