using EskitechApi.Models;
using EskitechApi.Services.ExcelServices;
using System.IO;
using EskitechApi.DTOs;
using EskitechApi.Data;

namespace EskitechApi.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IExcelService _excelService;
        private readonly EskitechDbContext _db;

        public ProductService(IExcelService excelService, EskitechDbContext db)
        {
            _excelService = excelService;
            _db = db;
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
        public List<Product> GetFullProductsDb()
        {
            return _db.Products.ToList();
        }

        public List<string> GetProductNamesDb()
        {
            var products = _db.Products.ToList();
            return products.Select(p => p.Name).ToList();
        }

        public List<ProductPriceDTO> GetProductWithPricesDb()
        {
            var products = _db.Products.ToList();
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

        public List<ProductStockDTO> GetProductWithStockDb()
        {
            var products = _db.Products.ToList();
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

        public int GetProductCountDb()
        {
            return _db.Products.ToList().Count();
        }

        public List<Product> GetProductsPaginated(int page, int pageSize)
        {
            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than 0");

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Pagesize must be greater than 0");

            var products = _db.Products.ToList();

            return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}