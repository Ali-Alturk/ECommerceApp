// Header and navigation functionality
document.addEventListener('DOMContentLoaded', function() {
    
    // Initialize Bootstrap dropdowns
    if (typeof bootstrap !== 'undefined') {
        // Initialize all dropdowns
        var dropdownElementList = [].slice.call(document.querySelectorAll('[data-bs-toggle="dropdown"]'));
        var dropdownList = dropdownElementList.map(function (dropdownToggleEl) {
            return new bootstrap.Dropdown(dropdownToggleEl, {
                autoClose: true
            });
        });
        
        // Add hover functionality for dropdowns on desktop
        if (window.innerWidth > 768) {
            const dropdowns = document.querySelectorAll('.navbar-nav .dropdown');
            dropdowns.forEach(dropdown => {
                const toggle = dropdown.querySelector('.dropdown-toggle');
                const menu = dropdown.querySelector('.dropdown-menu');
                
                if (toggle && menu) {
                    dropdown.addEventListener('mouseenter', function() {
                        if (!menu.classList.contains('show')) {
                            toggle.click();
                        }
                    });
                    
                    dropdown.addEventListener('mouseleave', function() {
                        if (menu.classList.contains('show')) {
                            toggle.click();
                        }
                    });
                }
            });
        }
    }
    
    // Search functionality
    const searchForm = document.querySelector('.search-container');
    const searchInput = document.getElementById('search-input');
    const searchButton = document.querySelector('.btn-search');
    
    if (searchForm && searchInput && searchButton) {
        // Handle search button click
        searchButton.addEventListener('click', function() {
            performSearch();
        });
        
        // Handle Enter key in search input
        searchInput.addEventListener('keypress', function(e) {
            if (e.key === 'Enter') {
                e.preventDefault();
                performSearch();
            }
        });
        
        function performSearch() {
            const searchTerm = searchInput.value.trim();
            if (searchTerm) {
                window.location.href = '/Product?search=' + encodeURIComponent(searchTerm);
            }
        }
    }
    
    // Mobile navbar toggle enhancements
    const navbarToggler = document.querySelector('.navbar-toggler');
    const navbarCollapse = document.querySelector('.navbar-collapse');
    
    if (navbarToggler && navbarCollapse) {
        // Close mobile menu when clicking on a link
        const navLinks = navbarCollapse.querySelectorAll('.nav-link');
        navLinks.forEach(link => {
            link.addEventListener('click', function() {
                if (window.innerWidth < 992) {
                    navbarCollapse.classList.remove('show');
                }
            });
        });
    }
    
    // Cart badge animation (if cart component is present)
    const cartBadge = document.querySelector('.cart-link .badge');
    if (cartBadge) {
        // Add pulse animation when cart is updated
        const observer = new MutationObserver(function(mutations) {
            mutations.forEach(function(mutation) {
                if (mutation.type === 'childList' || mutation.type === 'characterData') {
                    cartBadge.classList.add('animate-pulse');
                    setTimeout(() => {
                        cartBadge.classList.remove('animate-pulse');
                    }, 1000);
                }
            });
        });
        
        observer.observe(cartBadge, {
            childList: true,
            characterData: true,
            subtree: true
        });
    }
});

// Add CSS for pulse animation
const style = document.createElement('style');
style.textContent = `
    @keyframes pulse {
        0% { transform: scale(1); }
        50% { transform: scale(1.2); }
        100% { transform: scale(1); }
    }
    
    .animate-pulse {
        animation: pulse 0.5s ease-in-out;
    }
`;
document.head.appendChild(style);
