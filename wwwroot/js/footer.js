// Footer functionality
function searchCategory(categoryName) {
    window.location.href = '/Product?category=' + encodeURIComponent(categoryName);
}

// Newsletter subscription
document.addEventListener('DOMContentLoaded', function() {
    const newsletterBtn = document.querySelector('.btn-newsletter');
    const newsletterInput = document.querySelector('.newsletter-input');
    
    if (newsletterBtn && newsletterInput) {
        newsletterBtn.addEventListener('click', function() {
            const email = newsletterInput.value.trim();
            if (email.includes('@') && email.includes('.')) {
                alert('Thank you for subscribing to our newsletter!');
                newsletterInput.value = '';
            } else {
                alert('Please enter a valid email address.');
            }
        });
        
        newsletterInput.addEventListener('keypress', function(e) {
            if (e.key === 'Enter') {
                newsletterBtn.click();
            }
        });
    }
});
