using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [StringLength(200)]
        public string? Address { get; set; }

        [Display(Name = "City")]
        [StringLength(50)]
        public string? City { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(20)]
        public string? PostalCode { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        public string? Country { get; set; }
    }
}
