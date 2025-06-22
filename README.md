# E-Commerce Web Application

## Overview
This ECommerceApp was built using .NET Entity Framework with MVC pattern. This is a complete ASP.NET Core MVC e-commerce web application built with the following architecture:

## Project Structure

### Controllers
- **HomeController**: Public pages (Index, About, Contact)
- **ProductController**: Product listing and detail views
- **CartController**: Shopping cart management (Add/View/Remove items)
- **AccountController**: User authentication (Register, Login, Logout)
- **AdminController**: Administrative functions

### Models
- **Product**: Product information and details
- **Category**: Product categories
- **User**: Extended IdentityUser with additional fields
- **Order**: Customer orders and order items
- **CartItem**: Shopping cart items

### ViewModels
- **ProductListViewModel**: For product listing with filtering and pagination
- **CartViewModel**: For shopping cart display

### Areas
- **Admin Area**: Administrative panel for managing products, categories, and orders
- **User Area**: User dashboard for profile management and order history

## Features

### Public Features
- Home page with featured products
- Product browsing and searching
- Product details view
- User registration and authentication
- Contact form

### User Features (Authenticated)
- Shopping cart management
- User profile management
- Order history
- Password change

### Admin Features (Admin Role)
- Product management (CRUD operations)
- Category management
- Order management and status updates
- Dashboard with statistics
- User management

## Technology Stack
- ASP.NET Core 8.0 MVC
- Entity Framework Core
- ASP.NET Core Identity
- Bootstrap 5
- jQuery
- SQL Server (LocalDB)

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- SQL Server LocalDB (included with Visual Studio)

### Getting Started

1. **Clone or download the project**

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   - Open `appsettings.json`
   - Modify the connection string if needed

4. **Create and run migrations**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Open browser and navigate to `https://localhost:5001`

## Project Architecture

### Folder Structure
```
ECommerceApp/
├── Areas/
│   ├── Admin/
│   │   └── Controllers/
│   └── User/
│       └── Controllers/
├── Controllers/
├── Data/
├── Models/
├── ViewModels/
├── Views/
│   ├── Home/
│   ├── Product/
│   ├── Cart/
│   ├── Account/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
└── Program.cs
```

### Database Schema
The application uses Entity Framework Core with the following main entities:
- Users (extends IdentityUser)
- Products
- Categories
- Orders
- OrderItems
- CartItems

### Routing
- Default route: `{controller=Home}/{action=Index}/{id?}`
- Area routes: `{area:exists}/{controller=Home}/{action=Index}/{id?}`

## TODO: Implementation Notes

This is a scaffold/architecture project. The following items need to be implemented:

### Database Operations
- All Entity Framework CRUD operations in controllers
- Proper error handling and validation
- Data seeding for initial categories and admin user

### Views
- All view templates (.cshtml files)
- Admin area views
- User area views
- Responsive design implementation

### Business Logic
- Shopping cart calculations
- Order processing workflow
- Inventory management
- Payment integration (if needed)

### Security
- Role-based authorization implementation
- Input validation and sanitization
- CSRF protection
- Secure password requirements

### Additional Features
- Image upload for products
- Email notifications
- Advanced search and filtering
- Pagination implementation
- Logging and monitoring

## Contributing
This is a learning/template project. Feel free to extend and modify as needed.

## License
This project is for educational purposes.
