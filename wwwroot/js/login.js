// Login form functionality
document.addEventListener('DOMContentLoaded', function() {
    
    // Password toggle functionality
    window.toggleLoginPassword = function() {
        const passwordInput = document.querySelector('input[name="Password"]');
        const toggleIcon = document.getElementById('loginToggleIcon');
        
        if (passwordInput && toggleIcon) {
            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                toggleIcon.classList.replace('fa-eye', 'fa-eye-slash');
            } else {
                passwordInput.type = 'password';
                toggleIcon.classList.replace('fa-eye-slash', 'fa-eye');
            }
        }
    };
    
    // Enhanced form validation
    const form = document.querySelector('.auth-form');
    if (form) {
        form.addEventListener('submit', function(e) {
            const email = document.querySelector('input[name="Email"]').value.trim();
            const password = document.querySelector('input[name="Password"]').value.trim();
            
            if (!email || !password) {
                e.preventDefault();
                alert('Please fill in all required fields.');
                return false;
            }
            
            // Basic email validation
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(email)) {
                e.preventDefault();
                alert('Please enter a valid email address.');
                return false;
            }
        });
    }
});
