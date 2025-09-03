using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    [HttpGet]
    public async Task<IActionResult> GetDashboardData()
    {
        var products = await _productRepository.GetAllAsync();
        
        var totalProducts = products.Count();

        var totalStockValue = products.Sum(p => p.Price * p.QuantityInStock);

        var lowStockProducts = products.Where(p => p.QuantityInStock < 10);
        
        var categories = await _categoryRepository.GetAllAsync();
        
        // Prepara os dados do gráfico
        var productsByCategory = products
            .GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, Count = g.Count() })
            .ToList();

        var dashboardData = new
        {
            TotalProducts = totalProducts,
            TotalStockValue = totalStockValue,
            LowStockProducts = lowStockProducts,
            ProductsByCategory = productsByCategory
        };

        return Ok(dashboardData);
    }
}