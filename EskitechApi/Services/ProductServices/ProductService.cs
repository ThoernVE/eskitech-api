using EskitechApi.Models;
using EskitechApi.Services.ExcelServices;
using System.IO;
using EskitechApi.DTOs;

namespace EskitechApi.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IExcelService _excelService;

        public ProductService(IExcelService excelService)
        {
            _excelService = excelService;
        }

        //From Excel
        public List<Product> GetFullProductsExcel()
        {
            return _excelService.GetProductsFromExcel();
        }

        public List<string> GetProductNamesExcel()
        {
            var products = _excelService.GetProductsFromExcel();
            return products.Select(p => p.Name).ToList();
        }

        public List<ProductPriceDTO> GetProductWithPricesExcel()
        {
            var products = _excelService.GetProductsFromExcel();
            var dtoList = new List<ProductPriceDTO>();
            foreach (var product in products)
            {
                dtoList.Add(new ProductPriceDTO
                {
                    Name = product.Name,
                    Price = product.Price
                });
            }
            return dtoList;
        }

        public List<ProductStockDTO> GetProductWithStockExcel()
        {
            var products = _excelService.GetProductsFromExcel();
            var dtoList = new List<ProductStockDTO>();
            foreach (var product in products)
            {
                dtoList.Add(new ProductStockDTO
                {
                    Name = product.Name,
                    Stock = product.Stock
                });
            }
            return dtoList;
        }
  
        public int GetProductCountExcel()
        {
            var products = _excelService.GetProductsFromExcel();
            return products.Count();
        }

        //From DB
    }
}