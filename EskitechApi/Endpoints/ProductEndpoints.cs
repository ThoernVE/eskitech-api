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
            })
            .WithName("GetProductsFromExcel")
            .WithSummary("Fetch all products from Excel-file")
            .WithDescription("Retrieves a full list of products from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

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
            })
            .WithName("GetProductnamesFromExcel")
            .WithSummary("Fetch all productnames from Excel-file")
            .WithDescription("Retrieves a full list of only productnames from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

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
            })
            .WithName("GetProductPricesFromExcel")
            .WithSummary("Fetch all productnames with prices from Excel-file")
            .WithDescription("Retrieves a full list of productnames and prices from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

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
            })
            .WithName("GetProductStockFromExcel")
            .WithSummary("Fetch all productnames with stock from Excel-file")
            .WithDescription("Retrieves a full list of productnames and stock from the Excelfile currently in the datafolder. Retrieves empty list if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

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
            })
            .WithName("GetProductCountFromExcel")
            .WithSummary("Get count of all products from Excel-file")
            .WithDescription("Retrieves a count of products from the Excelfile currently in the datafolder. Retrieves 0 if no products are found. Returns 404 if file source is missing and 500 if something unexpected went wrong");

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
            .DisableAntiforgery()
            .WithName("UploadExcelToDatabase")
            .WithSummary("Upload Excel-file to the database")
            .WithDescription("Uploads a Excel-file to a SQL database. Does not upload products with the same name. Returns 400 if file cannot be uploaded");

            //DB endpoints
            app.MapGet("/products/db", async (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching products from database");
                    return Results.Ok(await productService.GetFullProductsDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductsFromDatabase")
            .WithSummary("Fetch all products from the database")
            .WithDescription("Retrieves a full list of products from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

            app.MapGet("/products/db/paginated", async (IProductService productService, ILogger<Program> logger, int page = 1, int pageSize = 10) =>
            {
                try
                {
                    logger.LogInformation("Fetching {pageSize} products from page {page} from database", pageSize, page);
                    return Results.Ok(await productService.GetProductsPaginated(page, pageSize));
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
            })
            .WithName("GetPaginatedProductsFromDatabase")
            .WithSummary("Fetch products from the database with pagination")
            .WithDescription("Retrieves a subset of products from the database using paging parameters. Provide a `page` number (starting at 1) and a `pageSize` (greater than 0). If page or pageSize are invalid, a 400 Bad Request is returned. If the requested page exceeds the number of products available, an empty list is returned. Returns 500 if something unexpected goes wrong.");

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
            })
            .WithName("GetProductNamesFromDatabase")
            .WithSummary("Fetch all productnames from the database")
            .WithDescription("Retrieves a full list of productnames from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

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
            })
            .WithName("GetProductPricesFromDatabase")
            .WithSummary("Fetch all productnames and prices from the database")
            .WithDescription("Retrieves a full list of productnames and prices from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

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
            })
            .WithName("GetProductStockFromDatabase")
            .WithSummary("Fetch all productnames and stock from the database")
            .WithDescription("Retrieves a full list of productnames and stock from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

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
            })
            .WithName("GetProductCountFromDatabase")
            .WithSummary("Fetch count of all products in database")
            .WithDescription("Retrieves a count products from the database. Retrieves 0 if no products are found. Returns 500 if something unexpected went wrong");
        }
    }
}