using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Product
        public async Task<IActionResult> Index(int? categoryId, string searchTerm, int page = 1, string sortBy = "name", string sortOrder = "asc")
        {
            var viewModel = new ProductListViewModel
            {
                SelectedCategoryId = categoryId,
                SearchTerm = searchTerm ?? string.Empty,
                CurrentPage = page,
                SortBy = sortBy,
                SortOrder = sortOrder
            };

            // TODO: Load products from database with filtering, searching, and pagination
            // viewModel.Products = await GetFilteredProducts(categoryId, searchTerm, page, sortBy, sortOrder);
            // viewModel.Categories = await _context.Categories.Where(c => c.IsActive).ToListAsync();
            // viewModel.TotalPages = CalculateTotalPages(categoryId, searchTerm, viewModel.PageSize);

            return View(viewModel);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO: Load product from database
            // var product = await _context.Products
            //     .Include(p => p.Category)
            //     .FirstOrDefaultAsync(m => m.Id == id && m.IsActive);

            // if (product == null)
            // {
            //     return NotFound();
            // }

            // return View(product);
            
            return View();
        }

        // GET: Product/Category/5
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            // TODO: Load products by category
            return RedirectToAction(nameof(Index), new { categoryId = id, page = page });
        }

        // GET: Product/Search
        public async Task<IActionResult> Search(string term, int page = 1)
        {
            // TODO: Search products
            return RedirectToAction(nameof(Index), new { searchTerm = term, page = page });
        }

        private async Task<IEnumerable<Product>> GetFilteredProducts(int? categoryId, string searchTerm, int page, string sortBy, string sortOrder)
        {
            // TODO: Implement product filtering logic
            return new List<Product>();
        }

        private int CalculateTotalPages(int? categoryId, string searchTerm, int pageSize)
        {
            // TODO: Calculate total pages based on filters
            return 1;
        }
    }
}
