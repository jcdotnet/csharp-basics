using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using FluentValidation;

namespace eCommerce.API.Endpoints
{
    public static class Products
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            // GET /api/products
            app.MapGet("api/products", async (IProductsService service) =>
            {
                var products = await service.GetProducts();
                return Results.Ok(products);
            });

            // GET /api/products/search/product-id/00000000-0000-0000-0000-000000000000
            app.MapGet("api/products/search/product-id/{ProductId:guid}", 
                async (IProductsService service, Guid ProductId) =>
            {
                var product = await service.GetProduct(product => product.ProductId == ProductId);
                if (product == null) return Results.NotFound();
                return Results.Ok(product);
            });

            // GET /api/products/search/xxxxx
            app.MapGet("api/products/search/{Search}",
                async (IProductsService service, string Search) =>
            {
                var productsByName = await service.GetProducts(
                    //product => product.ProductName.Contains(Search, StringComparison.OrdinalIgnoreCase)
                    product => product.ProductName.ToLower().Contains(Search.ToLower())

                );

                var productsByCategory = await service.GetProducts(
                    //product => product.Category.Contains(Search, StringComparison.OrdinalIgnoreCase)
                    product => product.Category.ToLower().IndexOf(Search.ToLower()) >= 0
                );
                var products = productsByName.Union(productsByCategory);
                return Results.Ok(products);
            });

            // POST /api/products
            app.MapPost("api/products", async (IProductsService service, 
                ProductAddRequest productAddRequest,
                IValidator<ProductAddRequest> validator) =>
            {
                var validationResult = await validator.ValidateAsync(productAddRequest);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.GroupBy(v => v.PropertyName).ToDictionary(
                        group => group.Key, group => group.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                var product = await service.AddProduct(productAddRequest);
                if (product is null) return Results.Problem("Product could not be added");
                return Results.Created($"api/products/search/product-id/{product.ProductId}", product);
            });

            // PUT /api/products/
            app.MapPut("api/products", async (IProductsService service,
                ProductUpdateRequest productUpdateRequest,
                IValidator<ProductUpdateRequest> validator) =>
            {
                var validationResult = await validator.ValidateAsync(productUpdateRequest);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.GroupBy(v => v.PropertyName).ToDictionary(
                        group => group.Key, group => group.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                var product = await service.UpdateProduct(productUpdateRequest);
                if (product is null) return Results.Problem("Product could not be updated");
                return Results.Ok(product);
            });

            // DELETE /api/products/
            app.MapDelete("api/products/{ProductId:guid}", async (IProductsService service,
                Guid ProductId) =>
            {
                var isDeleted = await service.DeleteProduct(ProductId);
                if (!isDeleted) return Results.Problem("Product could not be updated");
                return Results.Ok(isDeleted);
            });

            return app;
        }
    }
}
