using EskitechApi.Services.ExcelServices;
using EskitechApi.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace EskitechApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            //Excel endpoints
            app.MapGet("/products/excel", (IProductService productService, ILogger<Program> logger) =>
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
            });

            app.MapGet("/products/excel/names", (IProductService productService, ILogger<Program> logger) =>
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
            });

            app.MapGet("/products/excel/prices", (IProductService productService, ILogger<Program> logger) =>
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
            });

            app.MapGet("/products/excel/stock", (IProductService productService, ILogger<Program> logger) =>
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
            });

            app.MapGet("/products/excel/count", (IProductService productService, ILogger<Program> logger) =>
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
            });

            //Add products to DB from excel
            app.MapPost("/products/upload", async (IExcelService excelService, ILogger<Program> logger, IFormFile file) =>
            {
                try
                {
                    logger.LogInformation("File saved to database");
                    await excelService.ReadToDatabase(file);
                    return Results.Ok("File saved to database.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to read file into database");
                    return Results.BadRequest("Failed to read file.");
                }
            })
            .Accepts<IFormFile>("multipart/form-data")
            .DisableAntiforgery();

            //DB endpoints
            app.MapGet("/products/db", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching products from database");
                    return Results.Ok(productService.GetFullProductsDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/products/db/paginated", (IProductService productService, ILogger<Program> logger, int page = 1, int pageSize = 10) =>
            {
                try
                {
                    logger.LogInformation("Fetching {pageSize} products from page {page} from database", pageSize, page);
                    return Results.Ok(productService.GetProductsPaginated(page, pageSize));
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/products/db/names", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames from database");
                    return Results.Ok(productService.GetProductNamesDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/products/db/prices", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames and prices from database");
                    return Results.Ok(productService.GetProductWithPricesDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/products/db/stock", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames and stock from database");
                    return Results.Ok(productService.GetProductWithStockDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            });

            app.MapGet("/products/db/count", (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productcount from database");
                    return Results.Ok(productService.GetProductCountDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            });
        }
    }
}