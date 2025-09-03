using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _repository;

    public CategoriesController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(string id)
    {
        var item = await _repository.GetAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> AddCategory(Category item)
    {
        await _repository.AddAsync(item);
        return CreatedAtAction(nameof(GetCategory), new { id = item.Id }, item);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(string id, Category category)
    {
        if (category.Id == null || id != category.Id)
        {
            return BadRequest();
        }
        
        var result = await _repository.UpdateAsync(category);
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var item = await _repository.GetAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        await _repository.RemoveAsync(id);
        return NoContent();
    }
}