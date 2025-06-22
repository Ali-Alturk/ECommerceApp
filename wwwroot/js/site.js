// E-Commerce App JavaScript Functions

// Cart functionality
function addToCart(productId, quantity = 1) {
    $.ajax({
        url: '/Cart/AddToCart',
        type: 'POST',
        data: {
            productId: productId,
            quantity: quantity
        },
        success: function (response) {
            if (response.success) {
                updateCartCount();
                showNotification(response.message, 'success');
            } else {
                showNotification(response.message, 'error');
            }
        },
        error: function () {
            showNotification('An error occurred while adding item to cart.', 'error');
        }
    });
}

function updateCartQuantity(cartItemId, quantity) {
    $.ajax({
        url: '/Cart/UpdateQuantity',
        type: 'POST',
        data: {
            cartItemId: cartItemId,
            quantity: quantity
        },
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                showNotification(response.message, 'error');
            }
        },
        error: function () {
            showNotification('An error occurred while updating cart.', 'error');
        }
    });
}

function removeFromCart(cartItemId) {
    if (confirm('Are you sure you want to remove this item from cart?')) {
        $.ajax({
            url: '/Cart/RemoveFromCart',
            type: 'POST',
            data: {
                cartItemId: cartItemId
            },
            success: function (response) {
                if (response.success) {
                    location.reload();
                } else {
                    showNotification(response.message, 'error');
                }
            },
            error: function () {
                showNotification('An error occurred while removing item.', 'error');
            }
        });
    }
}

function updateCartCount() {
    $.ajax({
        url: '/Cart/GetCartCount',
        type: 'GET',
        success: function (response) {
            $('#cart-count').text(response.count);
        }
    });
}

// Notification system
function showNotification(message, type) {
    const alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
    const alertHtml = `
        <div class="alert ${alertClass} alert-dismissible fade show" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;
    
    const container = $('main').first();
    container.prepend(alertHtml);
    
    // Auto-dismiss after 5 seconds
    setTimeout(function() {
        $('.alert').fadeOut();
    }, 5000);
}

// Admin functionality
function updateOrderStatus(orderId, status) {
    $.ajax({
        url: '/Admin/UpdateOrderStatus',
        type: 'POST',
        data: {
            orderId: orderId,
            status: status
        },
        success: function (response) {
            if (response.success) {
                showNotification(response.message, 'success');
                location.reload();
            } else {
                showNotification(response.message, 'error');
            }
        },
        error: function () {
            showNotification('An error occurred while updating order status.', 'error');
        }
    });
}

// Initialize on document ready
$(document).ready(function() {
    // Update cart count on page load
    updateCartCount();
    
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
    
    // Quantity input handlers
    $('.quantity-input').on('change', function() {
        const cartItemId = $(this).data('cart-item-id');
        const quantity = parseInt($(this).val());
        
        if (quantity > 0) {
            updateCartQuantity(cartItemId, quantity);
        }
    });
    
    // Add to cart button handlers
    $('.btn-add-to-cart').on('click', function(e) {
        e.preventDefault();
        const productId = $(this).data('product-id');
        const quantity = parseInt($('#quantity').val()) || 1;
        addToCart(productId, quantity);
    });
    
    // Remove from cart button handlers
    $('.btn-remove-from-cart').on('click', function(e) {
        e.preventDefault();
        const cartItemId = $(this).data('cart-item-id');
        removeFromCart(cartItemId);
    });
});
