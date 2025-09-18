using EskitechApi.Services.ProductServices;

namespace EskitechApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGet("/products", (IProductService productService) => 
            {
                return Results.Ok(productService.GetFullProductsExcel());
            });

            app.MapGet("/products/names", (IProductService productService) =>
            {
                return Results.Ok(productService.GetProductNamesExcel());
            });

            app.MapGet("/products/prices", (IProductService productService) =>
            {
                return Results.Ok(productService.GetProductWithPricesExcel());
            });

            app.MapGet("/products/stock", (IProductService productService) =>
            {
                return Results.Ok(productService.GetProductWithStockExcel());
            });

            app.MapGet("/products/count", (IProductService productService) =>
            {
                return Results.Ok(productService.GetProductCountExcel());
            });
        }
    }
}