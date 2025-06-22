using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Models;

namespace ECommerceApp.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            
            // Seed Roles
            await SeedRolesAsync(roleManager);
            
            // Seed Admin User
            await SeedAdminUserAsync(userManager);
            
            // Seed Categories
            await SeedCategoriesAsync(context);
            
            // Seed Products
            await SeedProductsAsync(context);
        }
        
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User" };
            
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        
        private static async Task SeedAdminUserAsync(UserManager<User> userManager)
        {
            var adminEmail = "admin@ecommerce.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator",
                    Address = "123 Admin Street",
                    City = "Admin City",
                    PostalCode = "12345",
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now
                };
                
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
        
        private static async Task SeedCategoriesAsync(ApplicationDbContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category 
                    { 
                        Name = "Electronics", 
                        Description = "Electronic devices and gadgets",
                        CreatedDate = DateTime.Now
                    },
                    new Category 
                    { 
                        Name = "Clothing", 
                        Description = "Fashion and apparel items",
                        CreatedDate = DateTime.Now
                    },
                    new Category 
                    { 
                        Name = "Books", 
                        Description = "Books and educational materials",
                        CreatedDate = DateTime.Now
                    },
                    new Category 
                    { 
                        Name = "Home & Garden", 
                        Description = "Home improvement and gardening supplies",
                        CreatedDate = DateTime.Now
                    },
                    new Category 
                    { 
                        Name = "Sports & Outdoors", 
                        Description = "Sports equipment and outdoor gear",
                        CreatedDate = DateTime.Now
                    }
                };
                
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }
        
        private static async Task SeedProductsAsync(ApplicationDbContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                var electronicsCategory = await context.Categories.FirstAsync(c => c.Name == "Electronics");
                var clothingCategory = await context.Categories.FirstAsync(c => c.Name == "Clothing");
                var booksCategory = await context.Categories.FirstAsync(c => c.Name == "Books");
                var homeCategory = await context.Categories.FirstAsync(c => c.Name == "Home & Garden");
                var sportsCategory = await context.Categories.FirstAsync(c => c.Name == "Sports & Outdoors");
                
                var products = new List<Product>
                {                    new Product
                    {
                        Name = "Smartphone Pro Max",
                        Description = "Latest flagship smartphone with advanced features",
                        Price = 999.99m,
                        Stock = 50,
                        CategoryId = electronicsCategory.Id,
                        ImageUrl = "/images/smartphone.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Wireless Headphones",
                        Description = "Premium noise-cancelling wireless headphones",
                        Price = 299.99m,
                        Stock = 30,
                        CategoryId = electronicsCategory.Id,
                        ImageUrl = "/images/headphones.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Gaming Laptop",
                        Description = "High-performance gaming laptop with RTX graphics",
                        Price = 1599.99m,
                        Stock = 15,
                        CategoryId = electronicsCategory.Id,
                        ImageUrl = "/images/laptop.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Designer T-Shirt",
                        Description = "Premium cotton designer t-shirt",
                        Price = 49.99m,
                        Stock = 100,
                        CategoryId = clothingCategory.Id,
                        ImageUrl = "/images/tshirt.jpg",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Classic Jeans",
                        Description = "Comfortable classic fit jeans",
                        Price = 79.99m,
                        Stock = 75,
                        CategoryId = clothingCategory.Id,
                        ImageUrl = "/images/jeans.jpg",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Programming Guide",
                        Description = "Comprehensive guide to modern programming",
                        Price = 39.99m,
                        Stock = 40,
                        CategoryId = booksCategory.Id,
                        ImageUrl = "/images/book.jpg",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Coffee Maker",
                        Description = "Automatic drip coffee maker with timer",
                        Price = 129.99m,
                        Stock = 25,
                        CategoryId = homeCategory.Id,
                        ImageUrl = "/images/coffeemaker.jpg",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Running Shoes",
                        Description = "Professional running shoes with advanced cushioning",
                        Price = 149.99m,
                        Stock = 60,
                        CategoryId = sportsCategory.Id,
                        ImageUrl = "/images/runningshoes.jpg",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    }
                };
                
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
