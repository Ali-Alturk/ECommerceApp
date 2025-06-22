using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Data;

namespace ECommerceApp.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoryMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .Take(10)
                .ToListAsync();

            return View(categories);
        }
    }
}
