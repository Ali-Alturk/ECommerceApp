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
        }        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var viewModel = new CartViewModel
            {
                CartItems = cartItems
            };

            return View(viewModel);
        }        // POST: Cart/AddToCart
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
                // Check if product exists and is active
                var product = await _context.Products.FindAsync(productId);
                if (product == null || !product.IsActive)
                {
                    return Json(new { success = false, message = "Product not found." });
                }

                // Check if item already in cart
                var existingCartItem = await _context.CartItems
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

                if (existingCartItem != null)
                {
                    // Update existing quantity
                    existingCartItem.Quantity += quantity;
                    if (existingCartItem.Quantity > product.Stock)
                    {
                        return Json(new { success = false, message = "Insufficient stock available." });
                    }
                }
                else
                {
                    // Add new item to cart
                    if (quantity > product.Stock)
                    {
                        return Json(new { success = false, message = "Insufficient stock available." });
                    }                    var cartItem = new CartItem
                    {
                        UserId = userId,
                        ProductId = productId,
                        Quantity = quantity,
                        AddedDate = DateTime.Now
                    };
                    _context.CartItems.Add(cartItem);
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Item added to cart successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to cart");
                return Json(new { success = false, message = "An error occurred while adding item to cart." });
            }
        }        // POST: Cart/UpdateQuantity
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
                // Find cart item by id and user
                var cartItem = await _context.CartItems
                    .Include(c => c.Product)
                    .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

                if (cartItem == null)
                {
                    return Json(new { success = false, message = "Cart item not found." });
                }

                if (quantity <= 0)
                {
                    // Remove item if quantity is 0 or negative
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    // Validate stock availability
                    if (quantity > cartItem.Product.Stock)
                    {
                        return Json(new { success = false, message = "Insufficient stock available." });
                    }
                    cartItem.Quantity = quantity;
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Cart updated successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart quantity");
                return Json(new { success = false, message = "An error occurred while updating cart." });
            }
        }        // POST: Cart/RemoveFromCart
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
                // Find cart item by id and user
                var cartItem = await _context.CartItems
                    .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

                if (cartItem == null)
                {
                    return Json(new { success = false, message = "Cart item not found." });
                }

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

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

            var count = await _context.CartItems
                .Where(c => c.UserId == userId)
                .SumAsync(c => c.Quantity);

            return Json(new { count = count });
        }        // POST: Cart/ClearCart
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
                // Remove all cart items for user
                var cartItems = await _context.CartItems
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

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
