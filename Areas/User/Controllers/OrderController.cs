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
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ApplicationDbContext context, UserManager<Models.User> userManager, ILogger<OrderController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: User/Order
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = _userManager.GetUserId(User);
            
            // TODO: Load user orders with pagination
            // var orders = await _context.Orders
            //     .Where(o => o.UserId == userId)
            //     .Include(o => o.OrderItems)
            //     .ThenInclude(oi => oi.Product)
            //     .OrderByDescending(o => o.OrderDate)
            //     .ToListAsync();

            return View();
        }

        // GET: User/Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            // TODO: Load order details for current user
            // var order = await _context.Orders
            //     .Include(o => o.OrderItems)
            //     .ThenInclude(oi => oi.Product)
            //     .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            // if (order == null)
            // {
            //     return NotFound();
            // }

            return View();
        }

        // GET: User/Order/Reorder/5
        public async Task<IActionResult> Reorder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            try
            {
                // TODO: Implement reorder functionality
                // 1. Load the original order
                // 2. Add all items back to cart
                // 3. Redirect to cart

                TempData["Message"] = "Items added to cart successfully!";
                return RedirectToAction("Index", "Cart", new { area = "" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reordering");
                TempData["Error"] = "An error occurred while adding items to cart.";
                return RedirectToAction(nameof(Details), new { id = id });
            }
        }

        // POST: User/Order/CancelOrder/5
        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var userId = _userManager.GetUserId(User);

            try
            {
                // TODO: Implement order cancellation
                // 1. Check if order belongs to user and can be cancelled
                // 2. Update order status to cancelled
                // 3. Restore product stock if necessary

                return Json(new { success = true, message = "Order cancelled successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling order");
                return Json(new { success = false, message = "An error occurred while cancelling the order." });
            }
        }
    }
}
