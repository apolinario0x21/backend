using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        Product? item = await _repository.GetAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(string id, Product product)
    {
        if (product.Id == null || id != product.Id)
        {
            return BadRequest();
        }
        
        if (!await _repository.UpdateAsync(product))
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        Product? item = await _repository.GetAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        await _repository.RemoveAsync(id);
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            await _repository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        return BadRequest(ModelState);
    }
    
    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
    {
        var products = await _repository.GetAllAsync();
        return Ok(products.Where(
            p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase)));
    }
}