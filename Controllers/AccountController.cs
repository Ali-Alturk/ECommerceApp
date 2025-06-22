using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: Account/Register
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl ?? "/");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: Account/Login
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Profile
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Address = user.Address,
                City = user.City,
                PostalCode = user.PostalCode
            };

            return View(model);
        }

        // POST: Account/Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.City = model.City;
                    user.PostalCode = model.PostalCode;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["Message"] = "Profile updated successfully!";
                        return RedirectToAction(nameof(Profile));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public class ProfileViewModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [StringLength(10)]
        public string PostalCode { get; set; } = string.Empty;
    }
}
