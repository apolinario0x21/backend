using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly IProductRepository repository = new ProductRepository();

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        return Ok(repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        Product? item = repository.Get(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet("category/{category}")]
    public ActionResult<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        return Ok(repository.GetAll().Where(
            p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase)));
    }

    [HttpPost]
    public ActionResult<Product> PostProduct(Product item)
    {
        item = repository.Add(item);
        return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        if (!repository.Update(product))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        Product? item = repository.Get(id);
        if (item == null)
        {
            return NotFound();
        }

        repository.Remove(id);
        return NoContent();
    }
}