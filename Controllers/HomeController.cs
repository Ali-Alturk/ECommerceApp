using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Models;
using ECommerceApp.Data;
using ECommerceApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }        public async Task<IActionResult> Index()
        {
            // Load featured products from database
            var featuredProducts = await _context.Products
                .Where(p => p.IsActive && p.IsFeatured)
                .Include(p => p.Category)
                .Take(8)
                .ToListAsync();

            var viewModel = new ProductListViewModel
            {
                Products = featuredProducts
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }        public IActionResult Contact()
        {
            return View(new ContactViewModel());
        }
        
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Here you could send an email, save to database, etc.
                // For now, we'll just show a success message
                TempData["Message"] = $"Thank you {model.Name}! Your message has been received. We will get back to you at {model.Email} soon.";
                return RedirectToAction(nameof(Contact));
            }
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
