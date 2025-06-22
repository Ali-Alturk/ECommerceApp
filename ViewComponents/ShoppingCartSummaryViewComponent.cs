using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp.ViewComponents
{
    public class ShoppingCartSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ShoppingCartSummaryViewComponent(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
                
                var cartItems = await _context.CartItems
                    .Where(c => c.UserId == userId)
                    .Include(c => c.Product)
                    .ToListAsync();

                var itemCount = cartItems.Sum(c => c.Quantity);
                var total = cartItems.Sum(c => c.Product.Price * c.Quantity);

                return View(new { ItemCount = itemCount, Total = total });
            }

            return View(new { ItemCount = 0, Total = 0m });
        }
    }
}
