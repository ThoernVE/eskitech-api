using EskitechApi.Services.ExcelServices;
using EskitechApi.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EskitechApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            //Excel endpoints
            app.MapGet("/products/", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching products from Excelfile");
                    return Results.Ok(productService.GetFullProductsExcel());
                }
                catch (FileNotFoundException ex)
                {
                    logger.LogError(ex, "File could not be found");
                    return Results.NotFound("File could not be found");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from Excelfile");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProducts")
            .WithSummary("Fetch all products from Excel-file")
            .WithDescription("Retrieves a full list of products from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

            app.MapGet("/products/names", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames from Excelfile");
                    return Results.Ok(productService.GetProductNamesExcel());
                }
                catch (FileNotFoundException ex)
                {
                    logger.LogError(ex, "File could not be found");
                    return Results.NotFound("File could not be found");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from Excelfile");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductNames")
            .WithSummary("Fetch all productnames from Excel-file")
            .WithDescription("Retrieves a full list of only productnames from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

            app.MapGet("/products/prices", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames and prices from Excelfile");
                    return Results.Ok(productService.GetProductWithPricesExcel());
                }
                catch (FileNotFoundException ex)
                {
                    logger.LogError(ex, "File could not be found");
                    return Results.NotFound("File could not be found");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from Excelfile");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductPrices")
            .WithSummary("Fetch all productnames with prices from Excel-file")
            .WithDescription("Retrieves a full list of productnames and prices from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

            app.MapGet("/products/stock", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames and stock from Excelfile");
                    return Results.Ok(productService.GetProductWithStockExcel());
                }
                catch (FileNotFoundException ex)
                {
                    logger.LogError(ex, "File could not be found");
                    return Results.NotFound("File could not be found");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from Excelfile");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductStock")
            .WithSummary("Fetch all productnames with stock from Excel-file")
            .WithDescription("Retrieves a full list of productnames and stock from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

            app.MapGet("/products/count", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productcount from Excelfile");
                    return Results.Ok(productService.GetProductCountExcel());
                }
                catch (FileNotFoundException ex)
                {
                    logger.LogError(ex, "File could not be found");
                    return Results.NotFound("File could not be found");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from Excelfile");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductCount")
            .WithSummary("Get count of all products from Excel-file")
            .WithDescription("Retrieves a count of products from the Excelfile currently in the datafolder. Retrieves 0 if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

        }
    }
}