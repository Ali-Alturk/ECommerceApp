using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;
        
        [Display(Name = "Subject")]
        public string? Subject { get; set; }
        
        [Required(ErrorMessage = "Message is required")]
        [Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;
    }
}
