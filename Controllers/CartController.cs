using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceApp.Data;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CartController> _logger;

        public CartController(ApplicationDbContext context, UserManager<User> userManager, ILogger<CartController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new CartViewModel();
            // TODO: Load cart items from database
            // viewModel.CartItems = await _context.CartItems
            //     .Include(c => c.Product)
            //     .Where(c => c.UserId == userId)
            //     .ToListAsync();

            return View(viewModel);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { success = false, message = "Please log in to add items to cart." });
            }

            try
            {
                // TODO: Implement add to cart logic
                // 1. Check if product exists and is active
                // 2. Check if item already in cart (update quantity) or add new item
                // 3. Validate stock availability
                // 4. Save changes to database

                return Json(new { success = true, message = "Item added to cart successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to cart");
                return Json(new { success = false, message = "An error occurred while adding item to cart." });
            }
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { success = false, message = "Unauthorized." });
            }

            try
            {
                // TODO: Implement update quantity logic
                // 1. Find cart item by id and user
                // 2. Validate new quantity
                // 3. Update quantity or remove if quantity is 0
                // 4. Save changes

                return Json(new { success = true, message = "Cart updated successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart quantity");
                return Json(new { success = false, message = "An error occurred while updating cart." });
            }
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { success = false, message = "Unauthorized." });
            }

            try
            {
                // TODO: Implement remove from cart logic
                // 1. Find cart item by id and user
                // 2. Remove item from database
                // 3. Save changes

                return Json(new { success = true, message = "Item removed from cart!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing item from cart");
                return Json(new { success = false, message = "An error occurred while removing item." });
            }
        }

        // GET: Cart/GetCartCount
        public async Task<IActionResult> GetCartCount()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { count = 0 });
            }

            // TODO: Get cart item count from database
            // var count = await _context.CartItems
            //     .Where(c => c.UserId == userId)
            //     .SumAsync(c => c.Quantity);

            var count = 0;
            return Json(new { count = count });
        }

        // POST: Cart/ClearCart
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Json(new { success = false, message = "Unauthorized." });
            }

            try
            {
                // TODO: Implement clear cart logic
                // 1. Remove all cart items for user
                // 2. Save changes

                return Json(new { success = true, message = "Cart cleared successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart");
                return Json(new { success = false, message = "An error occurred while clearing cart." });
            }
        }
    }
}
