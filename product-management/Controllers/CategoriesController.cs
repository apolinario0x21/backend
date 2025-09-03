using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;

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
    public ActionResult<IEnumerable<Category>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetById(int id)
    {
        var item = _repository.Get(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public ActionResult<Category> Create(Category item)
    {
        if (item == null)
        {
            return BadRequest();
        }
        item = _repository.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Category item)
    {
        if (id != item.Id || item == null)
        {
            return BadRequest();
        }
        var existingItem = _repository.Get(id);
        if (existingItem == null)
        {
            return NotFound();
        }
        _repository.Update(item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _repository.Get(id);
        if (item == null)
        {
            return NotFound();
        }
        _repository.Remove(id);
        return NoContent();
    }
}