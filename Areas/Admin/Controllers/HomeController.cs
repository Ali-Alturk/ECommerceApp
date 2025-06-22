using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: Load dashboard statistics
            var dashboardData = new AdminDashboardViewModel
            {
                // TotalProducts = await _context.Products.CountAsync(p => p.IsActive),
                // TotalCategories = await _context.Categories.CountAsync(c => c.IsActive),
                // TotalOrders = await _context.Orders.CountAsync(),
                // TotalUsers = await _context.Users.CountAsync(),
                // PendingOrders = await _context.Orders.CountAsync(o => o.Status == OrderStatus.Pending),
                // TodaysOrders = await _context.Orders.CountAsync(o => o.OrderDate.Date == DateTime.Today),
                // MonthlyRevenue = await _context.Orders
                //     .Where(o => o.OrderDate.Month == DateTime.Now.Month && o.OrderDate.Year == DateTime.Now.Year)
                //     .SumAsync(o => o.TotalAmount)
            };

            return View(dashboardData);
        }

        public async Task<IActionResult> Reports()
        {
            // TODO: Implement reports functionality
            return View();
        }

        public async Task<IActionResult> Settings()
        {
            // TODO: Implement admin settings
            return View();
        }
    }

    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int PendingOrders { get; set; }
        public int TodaysOrders { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public IEnumerable<Order> RecentOrders { get; set; } = new List<Order>();
        public IEnumerable<Product> LowStockProducts { get; set; } = new List<Product>();
    }
}
