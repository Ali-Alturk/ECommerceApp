using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        public int ProductId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        public DateTime AddedDate { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
