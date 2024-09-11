using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API;

public static class MapApiEndpoints
{
    private const string ProductsTag = "Products - Minimal Api";

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ShopContext _context) =>
        {
            return await _context.Products.ToArrayAsync();
        }).WithTags(ProductsTag);

        app.MapGet("/products/{id}", async (int id, ShopContext _context) =>
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(product);
        }).WithTags(ProductsTag).WithName("GetProduct");

        app.MapGet("/products/available", async (ShopContext _context) =>
            Results.Ok(await _context.Products.Where(p => p.IsAvailable).ToArrayAsync())
        ).WithTags(ProductsTag);

        app.MapPost("/products", async (ShopContext _context, Product product) =>
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Results.CreatedAtRoute(
                "GetProduct",
                new { id = product.Id },
                product);
        }).WithTags(ProductsTag);

        app.MapPut("/products/{id}", async (ShopContext _context, int id, Product product) =>
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
        }).WithTags(ProductsTag);

        app.MapDelete("/products/{id}", async (ShopContext _context, int id) =>
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return Results.NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Results.Ok(product);
        }).WithTags(ProductsTag);

        app.MapPost("/products/Delete", async (ShopContext _context, [FromQuery] int[] ids) =>
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
        }).WithTags(ProductsTag);

        return app;
    }
}
