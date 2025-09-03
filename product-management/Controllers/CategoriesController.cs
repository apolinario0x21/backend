using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
using System.Collections.Generic;

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
    public ActionResult<Category> GetById(string id)
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
        return CreatedAtAction(nameof(GetById), new { id = item.Id.ToString() }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, Category item)
    {
        if (item == null || item.Id.ToString() != id)
        {
            return BadRequest();
        }
        
        if (!_repository.Update(item))
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
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