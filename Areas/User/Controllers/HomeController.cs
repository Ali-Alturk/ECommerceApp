using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<Models.User> userManager, ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            
            // TODO: Load user dashboard data
            var dashboardData = new UserDashboardViewModel
            {
                // RecentOrders = await _context.Orders
                //     .Where(o => o.UserId == userId)
                //     .OrderByDescending(o => o.OrderDate)
                //     .Take(5)
                //     .ToListAsync(),
                // TotalOrders = await _context.Orders.CountAsync(o => o.UserId == userId),
                // CartItemCount = await _context.CartItems
                //     .Where(c => c.UserId == userId)
                //     .SumAsync(c => c.Quantity)
            };

            return View(dashboardData);
        }
    }

    public class UserDashboardViewModel
    {
        public IEnumerable<Order> RecentOrders { get; set; } = new List<Order>();
        public int TotalOrders { get; set; }
        public int CartItemCount { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
