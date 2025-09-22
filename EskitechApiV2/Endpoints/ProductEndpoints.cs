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
            app.MapGet("/products/", async (IProductService productService, ILogger<Program> logger) =>
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
            .WithName("GetProducts")
            .WithSummary("Fetch all products from the database")
            .WithDescription("Retrieves a full list of products from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

            app.MapGet("/products/paginated", async (IProductService productService, ILogger<Program> logger, LinkGenerator linker, HttpContext http, int page = 1, int pageSize = 10) =>
            {
                try
                {
                    logger.LogInformation("Fetching {pageSize} products from page {page} from database", pageSize, page);
                    var pagedResult = await productService.GetProductsPaginated(page, pageSize);

                    var response = new PaginationResponse<Product>
                    {
                        Page = pagedResult.Page,
                        PageSize = pagedResult.PageSize,
                        TotalCount = pagedResult.TotalCount,
                        TotalPages = pagedResult.TotalPages,
                        FirstPage = linker.GetUriByName(http, "GetPaginatedProducts", new { page = 1, pageSize }),
                        LastPage = linker.GetUriByName(http, "GetPaginatedProducts", new { page = pagedResult.TotalPages, pageSize }),
                        NextPage = pagedResult.Page < pagedResult.TotalPages
                        ? linker.GetUriByName(http, "GetPaginatedProducts", new { page = pagedResult.Page + 1, pageSize })
                        : null,
                        PreviousPage = pagedResult.Page > 1
                        ? linker.GetUriByName(http, "GetPaginatedProducts", new { page = pagedResult.Page - 1, pageSize })
                        : null,
                        Data = pagedResult.Data

                    };
                    return Results.Ok(response);
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
            .WithName("GetPaginatedProducts")
            .WithSummary("Fetch products from the database with pagination")
            .WithDescription("Retrieves a subset of products from the database using paging parameters. Provide a `page` number (starting at 1) and a `pageSize` (greater than 0). If page or pageSize are invalid, a 400 Bad Request is returned. If the requested page exceeds the number of products available, an empty list is returned. Returns 500 if something unexpected goes wrong.");

            app.MapGet("/products/names", async (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames from database");
                    return Results.Ok(await productService.GetProductNamesDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductNames")
            .WithSummary("Fetch all productnames from the database")
            .WithDescription("Retrieves a full list of productnames from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

            app.MapGet("/products/prices", async (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames and prices from database");
                    return Results.Ok(await productService.GetProductWithPricesDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductPrices")
            .WithSummary("Fetch all productnames and prices from the database")
            .WithDescription("Retrieves a full list of productnames and prices from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

            app.MapGet("/products/stock", async (IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productnames and stock from database");
                    return Results.Ok(await productService.GetProductWithStockDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductStock")
            .WithSummary("Fetch all productnames and stock from the database")
            .WithDescription("Retrieves a full list of productnames and stock from the database. Retrieves empty list if no products are found. Returns 500 if something unexpected went wrong");

            app.MapGet("/products/count", async(IProductService productService, ILogger<Program> logger) =>
            {
                try
                {
                    logger.LogInformation("Fetching productcount from database");
                    return Results.Ok(await productService.GetProductCountDb());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to fetch products from database");
                    return Results.StatusCode(500);
                }
            })
            .WithName("GetProductCount")
            .WithSummary("Fetch count of all products in database")
            .WithDescription("Retrieves a count products from the database. Retrieves 0 if no products are found. Returns 500 if something unexpected went wrong");
        }
    }
}