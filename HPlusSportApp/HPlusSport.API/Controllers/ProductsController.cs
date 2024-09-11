using HPlusSport.API.Data;
using HPlusSport.API.Data.Entity;
using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(ShopDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return Ok(await context.Products.AsNoTracking().ToArrayAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
    {
        var product = await context.Products
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAvailableProducts()
    {
        return await context.Products.AsNoTracking().Where(p => p.IsAvailable).ToArrayAsync();
    }

    [HttpPost]
    public async Task<ActionResult> PostProduct([FromBody] CreateProductRequest request)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var product = new Product()
        {
            Name = request.Name,
            Sku = request.Sku,
            Description = request.Description,
            Price = request.Price,
            IsAvailable = request.IsAvailable,
            CategoryId = request.CategoryId,
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = product.Id },
            product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutProduct([FromRoute] int id, [FromBody] UpdateProductRequest request)
    {
        var product = await context.Products
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        try
        {
            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.Price = request.Price ?? product.Price;
            product.IsAvailable = request.IsAvailable ?? product.IsAvailable;

            context.Products.Update(product);

            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Products.Any(p => p.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> DeleteMultiple([FromQuery] int[] ids)
    {
        var products = new List<Product>();

        foreach (var id in ids)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            products.Add(product);
        }

        context.Products.RemoveRange(products);
        await context.SaveChangesAsync();

        return Ok(products);
    }
}
