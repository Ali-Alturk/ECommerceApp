using ECommerceApp.Models;

namespace ECommerceApp.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalAmount => CartItems.Sum(item => item.Product.Price * item.Quantity);
        public int TotalItems => CartItems.Sum(item => item.Quantity);
        public bool HasItems => CartItems.Any();
    }
}
