using ECommerceApp.Models;

namespace ECommerceApp.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCategories { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PendingOrders { get; set; }
        public int CompletedOrders { get; set; }
        
        public List<Product> RecentProducts { get; set; } = new List<Product>();
        public List<Order> RecentOrders { get; set; } = new List<Order>();
        public Dictionary<string, int> OrdersByMonth { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, decimal> RevenueByMonth { get; set; } = new Dictionary<string, decimal>();
    }
}
