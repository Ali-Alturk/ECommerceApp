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
        }        // GET: Product
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

            // Load products from database with filtering, searching, and pagination
            var query = _context.Products.Include(p => p.Category).Where(p => p.IsActive);

            // Apply category filter
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "price" => sortOrder == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "date" => sortOrder == "desc" ? query.OrderByDescending(p => p.CreatedDate) : query.OrderBy(p => p.CreatedDate),
                _ => sortOrder == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name)
            };

            // Calculate pagination
            var totalProducts = await query.CountAsync();
            viewModel.TotalPages = (int)Math.Ceiling((double)totalProducts / viewModel.PageSize);

            // Apply pagination
            viewModel.Products = await query
                .Skip((page - 1) * viewModel.PageSize)
                .Take(viewModel.PageSize)
                .ToListAsync();

            // Load categories for filter dropdown
            viewModel.Categories = await _context.Categories.Where(c => c.IsActive).ToListAsync();

            return View(viewModel);
        }        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.IsActive);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);        }

        // GET: Product/Category/5
        public IActionResult Category(int id, int page = 1)
        {
            return RedirectToAction(nameof(Index), new { categoryId = id, page = page });
        }

        // GET: Product/Search
        public IActionResult Search(string term, int page = 1)
        {
            return RedirectToAction(nameof(Index), new { searchTerm = term, page = page });
        }
    }
}
