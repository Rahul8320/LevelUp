using DotnetCoreWebApi.Data;
using DotnetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ShopContext context) : ControllerBase
{
    private readonly ShopContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var allProducts = await _context.Products.ToListAsync();

        return Ok(allProducts);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }
}
