// E-Commerce Site JavaScript

// Initialize when document is ready
document.addEventListener('DOMContentLoaded', function() {
    initializeApp();
});

function initializeApp() {
    initializeTooltips();
    initializeCartFunctionality();
    initializeProductCards();
    initializeFormValidation();
}

// Initialize Bootstrap tooltips
function initializeTooltips() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

// Cart functionality
function initializeCartFunctionality() {
    // Add to cart buttons
    document.querySelectorAll('.add-to-cart').forEach(function(button) {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            var productId = this.getAttribute('data-product-id');
            var quantity = 1;
            
            // Get quantity from input if exists
            var quantityInput = document.querySelector(`#quantity-${productId}`);
            if (quantityInput) {
                quantity = parseInt(quantityInput.value) || 1;
            }
            
            addToCart(productId, quantity);
        });
    });
}

// Product card interactions
function initializeProductCards() {
    document.querySelectorAll('.product-card').forEach(function(card) {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-5px)';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
}

// Form validation
function initializeFormValidation() {
    var forms = document.querySelectorAll('.needs-validation');
    
    Array.prototype.slice.call(forms).forEach(function(form) {
        form.addEventListener('submit', function(event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });
}

// Notification system
function showNotification(message, type) {
    var notification = document.createElement('div');
    notification.className = `alert alert-${type === 'success' ? 'success' : 'danger'} alert-dismissible fade show position-fixed`;
    notification.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
    notification.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;
    
    document.body.appendChild(notification);
    
    setTimeout(function() {
        if (notification.parentNode) {
            notification.parentNode.removeChild(notification);
        }
    }, 5000);
}

// Cart functionality
function addToCart(productId, quantity) {
    showLoading();
    
    fetch('/Cart/AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({
            productId: parseInt(productId),
            quantity: parseInt(quantity)
        })
    })
    .then(response => response.json())
    .then(data => {
        hideLoading();
        if (data.success) {
            showNotification(data.message, 'success');
            updateCartCount();
        } else {
            showNotification(data.message, 'error');
        }
    })
    .catch(error => {
        hideLoading();
        console.error('Error:', error);
        showNotification('An error occurred while adding item to cart.', 'error');
    });
}

function updateCartQuantity(cartItemId, quantity) {
    showLoading();
    
    fetch('/Cart/UpdateQuantity', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({
            cartItemId: parseInt(cartItemId),
            quantity: parseInt(quantity)
        })
    })
    .then(response => response.json())
    .then(data => {
        hideLoading();
        if (data.success) {
            showNotification(data.message, 'success');
            location.reload(); // Refresh cart page
        } else {
            showNotification(data.message, 'error');
        }
    })
    .catch(error => {
        hideLoading();
        console.error('Error:', error);
        showNotification('An error occurred while updating cart.', 'error');
    });
}

function removeFromCart(cartItemId) {
    if (!confirm('Are you sure you want to remove this item from your cart?')) {
        return;
    }
    
    showLoading();
    
    fetch('/Cart/RemoveFromCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({
            cartItemId: parseInt(cartItemId)
        })
    })
    .then(response => response.json())
    .then(data => {
        hideLoading();
        if (data.success) {
            showNotification(data.message, 'success');
            location.reload(); // Refresh cart page
        } else {
            showNotification(data.message, 'error');
        }
    })
    .catch(error => {
        hideLoading();
        console.error('Error:', error);
        showNotification('An error occurred while removing item.', 'error');
    });
}

function updateCartCount() {
    fetch('/Cart/GetCartCount')
    .then(response => response.json())
    .then(data => {
        const cartBadge = document.querySelector('.cart-count');
        if (cartBadge) {
            cartBadge.textContent = data.count;
            cartBadge.style.display = data.count > 0 ? 'inline' : 'none';
        }
    })
    .catch(error => {
        console.error('Error updating cart count:', error);
    });
}

function searchProducts() {
    const searchInput = document.getElementById('search-input');
    if (searchInput && searchInput.value.trim()) {
        window.location.href = `/Product?searchTerm=${encodeURIComponent(searchInput.value.trim())}`;
    }
}

function showLoading() {
    const loadingOverlay = document.getElementById('loading-overlay');
    if (loadingOverlay) {
        loadingOverlay.style.display = 'flex';
    }
}

function hideLoading() {
    const loadingOverlay = document.getElementById('loading-overlay');
    if (loadingOverlay) {
        loadingOverlay.style.display = 'none';
    }
}
