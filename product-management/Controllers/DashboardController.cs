using Microsoft.AspNetCore.Mvc;
using ProductStore.Models; 

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public DashboardController(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpGet("summary")]
    public ActionResult<DashboardSummary> GetSummary()
    {
        var products = _productRepository.GetAll();
        var totalProducts = products.Count();
        var totalStockValue = products.Sum(p => p.Price * p.QuantityInStock);
        var categories = _categoryRepository.GetAll();

        var summary = new DashboardSummary
        {
            TotalProducts = totalProducts,
            TotalCategories = categories.Count(),
            TotalStockValue = totalStockValue
        };

        return Ok(summary);
    }
    
    [HttpGet("low-stock")]
    public ActionResult<IEnumerable<Product>> GetLowStockProducts()
    {
        var lowStockProducts = _productRepository.GetAll().Where(p => p.QuantityInStock <= 10);
        return Ok(lowStockProducts);
    }
}

public class DashboardSummary
{
    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
    public decimal TotalStockValue { get; set; }
}