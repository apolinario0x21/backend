using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
using System.Collections.Generic;
using MongoDB.Bson;

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
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(string id) 
    {
        Product? item = _repository.Get(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }
    

   [HttpPut("{id}")]
public IActionResult PutProduct(string id, [FromBody] Product product)
{
    // Tenta converter o ID da rota para um ObjectId
    if (!ObjectId.TryParse(id, out ObjectId objectId))
    {
        return BadRequest();
    }

    if (product.Id != objectId)
    {
        return BadRequest();
    }

    if (!_repository.Update(product))
    {
        return NotFound();
    }

    return NoContent();
}
    
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(string id) 
    {
        Product? item = _repository.Get(id);
        if (item == null)
        {
            return NotFound();
        }

        _repository.Remove(id);
        return NoContent();
    }
    
    [HttpPost]
    public ActionResult<Product> PostProduct(Product item)
    {
        item = _repository.Add(item);
        return CreatedAtAction(nameof(GetProduct), new { id = item.Id.ToString() }, item);
    }
    
    [HttpGet("category/{category}")]
    public ActionResult<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        return Ok(_repository.GetAll().Where(
            p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase)));
    }
}