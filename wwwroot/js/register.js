// Register form functionality
document.addEventListener('DOMContentLoaded', function() {
    
    // Password toggle functionality
    window.togglePassword = function(fieldName) {
        const field = document.getElementById(fieldName);
        const toggle = field.parentElement.querySelector('.password-toggle i');
        
        if (field.type === 'password') {
            field.type = 'text';
            toggle.classList.replace('fa-eye', 'fa-eye-slash');
        } else {
            field.type = 'password';
            toggle.classList.replace('fa-eye-slash', 'fa-eye');
        }
    };
    
    // Add password strength indicator
    const passwordField = document.getElementById('Password');
    if (passwordField) {
        passwordField.addEventListener('input', function() {
            const password = this.value;
            const requirements = document.querySelector('.password-requirements');
            
            // Basic password strength check
            let strength = 0;
            if (password.length >= 8) strength++;
            if (/[A-Z]/.test(password)) strength++;
            if (/[a-z]/.test(password)) strength++;
            if (/[0-9]/.test(password)) strength++;
            if (/[^A-Za-z0-9]/.test(password)) strength++;
            
            let strengthText = '';
            let strengthClass = '';
            
            if (password.length > 0) {
                if (strength < 2) {
                    strengthText = 'Weak';
                    strengthClass = 'text-danger';
                } else if (strength < 4) {
                    strengthText = 'Medium';
                    strengthClass = 'text-warning';
                } else {
                    strengthText = 'Strong';
                    strengthClass = 'text-success';
                }
                
                requirements.innerHTML = `<small class="${strengthClass}">Password strength: ${strengthText}</small>`;
            } else {
                requirements.innerHTML = '<small class="text-muted">Password must be at least 6 characters long</small>';
            }
        });
    }
});
