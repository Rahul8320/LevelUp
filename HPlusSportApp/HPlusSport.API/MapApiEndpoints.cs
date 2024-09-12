using Asp.Versioning;
using HPlusSport.API.Data;
using HPlusSport.API.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API;

public static class MapApiEndpoints
{
    private const string ProductsTag = "Products - Minimal Api";

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.NewApiVersionSet()
                                            .HasApiVersion(new ApiVersion(1, 0))
                                            .ReportApiVersions()
                                            .Build();

        app.MapGet("/products", async (ShopDbContext _context) =>
        {
            return await _context.Products.ToArrayAsync();
        })
            .WithTags(ProductsTag)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0));

        app.MapGet("/products/{id}", async (int id, ShopDbContext _context) =>
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(product);
        })
            .WithName("GetProduct")
            .WithTags(ProductsTag)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0)); ;

        app.MapGet("/products/available", async (ShopDbContext _context) =>
            Results.Ok(await _context.Products.Where(p => p.IsAvailable).ToArrayAsync())
        )
            .WithTags(ProductsTag)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0));

        app.MapPost("/products", async (ShopDbContext _context, Product product) =>
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Results.CreatedAtRoute(
                "GetProduct",
                new { id = product.Id },
                product);
        })
            .WithTags(ProductsTag)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0));

        app.MapPut("/products/{id}", async (ShopDbContext _context, int id, Product product) =>
        {
            if (id != product.Id)
            {
                return Results.BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.Id == id))
                {
                    return Results.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Results.NoContent();
        })
            .WithTags(ProductsTag)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0));

        app.MapDelete("/products/{id}", async (ShopDbContext _context, int id) =>
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return Results.NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Results.Ok(product);
        })
            .WithTags(ProductsTag)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0));

        app.MapPost("/products/Delete", async (ShopDbContext _context, [FromQuery] int[] ids) =>
        {
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return Results.NotFound();
                }

                products.Add(product);
            }

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();

            return Results.Ok(products);
        })
            .WithTags(ProductsTag)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0));

        return app;
    }
}
