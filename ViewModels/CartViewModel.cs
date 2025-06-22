using ECommerceApp.Models;

namespace ECommerceApp.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();
        public IEnumerable<CartItem> Items => CartItems; // Alias for backward compatibility
        
        public decimal TotalAmount => CartItems.Sum(item => item.Product.Price * item.Quantity);
        public int TotalItems => CartItems.Sum(item => item.Quantity);
        public bool HasItems => CartItems.Any();
        
        // Additional properties for cart calculations
        public decimal Subtotal => TotalAmount;
        public decimal ShippingCost => Subtotal > 100 ? 0 : 10; // Free shipping over $100
        public decimal Tax => Subtotal * 0.08m; // 8% tax rate
        public decimal Total => Subtotal + ShippingCost + Tax;
    }
}
