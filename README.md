# E-Commerce Backend API

A robust and scalable **E-Commerce Backend RESTful API** built with **ASP.NET Core**.  
This project serves as the backend system for an online shopping platform, handling authentication, product management, orders, basket operations, and payment-related workflows.

The API follows clean architecture principles and is designed to be consumed by modern frontend frameworks such as **Angular**, **React**, or **Vue**.

---

## 🚀 Features

### 🔐 Authentication & Authorization
- User Registration & Login
- JWT Token Authentication
- Account Activation
- Password Reset & Forgot Password
- Role-based Authorization
- Secure API Endpoints

### 🛒 E-Commerce Core
- Product Management
- Category Management
- Product Images
- Basket (Cart) Management
- Order Creation & Tracking
- Delivery Method Handling

### 📦 Orders
- Place Orders
- Retrieve Order History
- Order Details
- Order Status Tracking

### 📩 Communication
- Contact Us Messages Handling

### ⚙️ Technical Features
- RESTful API Design
- Entity Framework Core
- Repository Pattern
- Unit of Work
- Global Exception Handling
- Pagination & Filtering
- Swagger API Documentation
- Dependency Injection

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core Web API
- **Language:** C#
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Authentication:** JWT Bearer Tokens
- **Architecture:** Clean Architecture
- **API Documentation:** Swagger (OpenAPI)

---
```
└── 📁 ECom.API
    ├── 📁 .github
    │   └── 📁 workflows
    ├── 📁 ECom.API
    │   ├── 📁 Controllers
    │   │   ├── 📄 AuthController.cs
    │   │   ├── 📄 BasketController.cs
    │   │   ├── 📄 CategoriesController.cs
    │   │   ├── 📄 ContactController.cs
    │   │   ├── 📄 DeliveryMethodsController.cs
    │   │   ├── 📄 ErrorsController.cs
    │   │   ├── 📄 OrdersController.cs
    │   │   ├── 📄 PaymentsController.cs
    │   │   └── 📄 ProductsController.cs
    │   ├── 📁 Extensions
    │   │   └── 📄 ExceptionMiddlewareExtensions.cs
    │   ├── 📁 Helper
    │   │   └── 📄 ResponseApi.cs
    │   ├── 📁 Middleware
    │   │   └── 📄 ErrorHandlingMiddleware.cs
    │   ├── 📁 Properties
    │   │   └── ⚙️ launchSettings.json
    │   ├── 📄 ECom.API.csproj
    │   ├── 📄 ECom.API.csproj.user
    │   ├── 📄 ECom.API.http
    │   ├── 📄 Program.cs
    │   ├── ⚙️ appsettings.Development.json
    │   └── ⚙️ appsettings.json
    ├── 📁 ECom.BLL
    │   ├── 📁 DTOs
    │   │   ├── 📁 Pagination
    │   │   │   ├── 📄 ActivateAccountDto.cs
    │   │   │   └── 📄 PaginationResopnse.cs
    │   │   ├── 📄 CategoryDto.cs
    │   │   ├── 📄 ContactMessageDto.cs
    │   │   ├── 📄 DeliveryMethodDto.cs
    │   │   ├── 📄 EmailDto.cs
    │   │   ├── 📄 EmailStringBody.cs
    │   │   ├── 📄 ForgotPasswordDto.cs
    │   │   ├── 📄 InvoiceEmailDto.cs
    │   │   ├── 📄 LoginDto.cs
    │   │   ├── 📄 OrderItemDto.cs
    │   │   ├── 📄 ProductDto.cs
    │   │   ├── 📄 ProductParams.cs
    │   │   ├── 📄 RegisterDto.cs
    │   │   └── 📄 ResetPasswordDto.cs
    │   ├── 📁 Interfaces
    │   │   ├── 📄 IAuthService.cs
    │   │   ├── 📄 ICategoryServices.cs
    │   │   ├── 📄 ICurrentUserService.cs
    │   │   ├── 📄 ICustomerBasketSercvice.cs
    │   │   ├── 📄 IDeliveryMethodService.cs
    │   │   ├── 📄 IEmailService.cs
    │   │   ├── 📄 IImageService.cs
    │   │   ├── 📄 IInvoiceService.cs
    │   │   ├── 📄 IOrderService.cs
    │   │   ├── 📄 IPaymentService.cs
    │   │   └── 📄 IProductService.cs
    │   ├── 📁 Mapper
    │   │   ├── 📄 CategoryMapper.cs
    │   │   ├── 📄 DeliveryMethodProfile.cs
    │   │   ├── 📄 OrderProfile.cs
    │   │   └── 📄 ProductMapper.cs
    │   ├── 📁 Services
    │   │   ├── 📄 AuthService.cs
    │   │   ├── 📄 CategoryServices.cs
    │   │   ├── 📄 CurrentUserService.cs
    │   │   ├── 📄 CustomerBasketSercvice.cs
    │   │   ├── 📄 DeliveryMethodService.cs
    │   │   ├── 📄 EmailService.cs
    │   │   ├── 📄 ImageService.cs
    │   │   ├── 📄 InvoicePdfGenerator.cs
    │   │   ├── 📄 InvoiceService.cs
    │   │   ├── 📄 OrderService.cs
    │   │   ├── 📄 PaymentService.cs
    │   │   └── 📄 ProductService.cs
    │   └── 📄 ECom.BLL.csproj
    ├── 📁 ECom.DAL
    │   ├── 📁 Data
    │   │   └── 📄 AppDbContext.cs
    │   ├── 📁 Entities
    │   │   ├── 📁 BaseEntity
    │   │   │   └── 📄 Base.cs
    │   │   ├── 📁 Order
    │   │   │   ├── 📄 DeliveryMethod.cs
    │   │   │   ├── 📄 OrderItem.cs
    │   │   │   ├── 📄 Orders.cs
    │   │   │   ├── 📄 PaymentMethod.cs
    │   │   │   ├── 📄 ShippingAddress.cs
    │   │   │   └── 📄 Status.cs
    │   │   ├── 📄 Address.cs
    │   │   ├── 📄 AppUser.cs
    │   │   ├── 📄 BasketItem.cs
    │   │   ├── 📄 Category.cs
    │   │   ├── 📄 CustomBasket.cs
    │   │   ├── 📄 Photo.cs
    │   │   └── 📄 Product.cs
    │   ├── 📁 Interfaces
    │   │   ├── 📄 IBaseRepositories.cs
    │   │   └── 📄 IUnitOfWork.cs
    │   ├── 📁 Migrations
    │   │   ├── 📄 20251121062440_firstUpdate.Designer.cs
    │   │   ├── 📄 20251121062440_firstUpdate.cs
    │   │   ├── 📄 20251121092129_secound.Designer.cs
    │   │   ├── 📄 20251121092129_secound.cs
    │   │   ├── 📄 20251210071744_AddProductTypeRelationFix.Designer.cs
    │   │   ├── 📄 20251210071744_AddProductTypeRelationFix.cs
    │   │   ├── 📄 20251220062239_AppUserMigration.Designer.cs
    │   │   ├── 📄 20251220062239_AppUserMigration.cs
    │   │   ├── 📄 20251226041829_orderMigration.Designer.cs
    │   │   ├── 📄 20251226041829_orderMigration.cs
    │   │   ├── 📄 20251226051522_orderUpdated.Designer.cs
    │   │   ├── 📄 20251226051522_orderUpdated.cs
    │   │   ├── 📄 20251226052745_FixShippingAddress.Designer.cs
    │   │   ├── 📄 20251226052745_FixShippingAddress.cs
    │   │   ├── 📄 20251226053718_FixShippingAddress2.Designer.cs
    │   │   ├── 📄 20251226053718_FixShippingAddress2.cs
    │   │   ├── 📄 20251226055550_FixShippin.Designer.cs
    │   │   ├── 📄 20251226055550_FixShippin.cs
    │   │   ├── 📄 20251227092133_emailMigration.Designer.cs
    │   │   ├── 📄 20251227092133_emailMigration.cs
    │   │   ├── 📄 20251227103423_emailMigration2.Designer.cs
    │   │   ├── 📄 20251227103423_emailMigration2.cs
    │   │   ├── 📄 20251227151235_lastUpdated.Designer.cs
    │   │   ├── 📄 20251227151235_lastUpdated.cs
    │   │   └── 📄 AppDbContextModelSnapshot.cs
    │   ├── 📁 Repositories
    │   │   ├── 📄 BaseRepository.cs
    │   │   └── 📄 UnitOfWork.cs
    │   └── 📄 ECom.DAL.csproj
    ├── ⚙️ .gitattributes
    ├── ⚙️ .gitignore
    └── 📄 ECom.API.sln
```


---

## 🧩 Core Modules

### Authentication
- Register
- Login
- JWT Token Generation
- Account Confirmation
- Password Recovery

### Products
- CRUD Operations
- Pagination
- Filtering & Sorting
- Product Images Support

### Basket
- Create Basket
- Add / Remove Items
- Update Quantities

### Orders
- Create Order
- Get Orders by User
- Order Details
- Delivery Method Selection

---

## 🔐 Security

- JWT Authentication
- Secure Password Hashing
- Role-based Access Control
- Protected API Endpoints

---

## 🗄️ Database Design (ER Diagram)

```mermaid
erDiagram

    USER {
        int Id
        string UserName
        string Email
        string PasswordHash
    }

    ADDRESS {
        int Id
        string FirstName
        string LastName
        string Street
        string City
        string Country
        string ZipCode
    }

    PRODUCT {
        int Id
        string Name
        string Description
        decimal Price
        int Stock
    }

    CATEGORY {
        int Id
        string Name
    }

    PRODUCT_PHOTO {
        int Id
        string Url
    }

    BASKET {
        int Id
    }

    BASKET_ITEM {
        int Id
        int Quantity
    }

    ORDER {
        int Id
        datetime OrderDate
        decimal Subtotal
        string Status
    }

    ORDER_ITEM {
        int Id
        int Quantity
        decimal Price
    }

    DELIVERY_METHOD {
        int Id
        string ShortName
        decimal Cost
        string DeliveryTime
    }

    CONTACT_MESSAGE {
        int Id
        string Email
        string Message
    }

    USER ||--o{ ADDRESS : has
    USER ||--o{ ORDER : places
    USER ||--|| BASKET : owns

    CATEGORY ||--o{ PRODUCT : contains
    PRODUCT ||--o{ PRODUCT_PHOTO : has

    BASKET ||--o{ BASKET_ITEM : includes
    PRODUCT ||--o{ BASKET_ITEM : added_to

    ORDER ||--o{ ORDER_ITEM : contains
    PRODUCT ||--o{ ORDER_ITEM : ordered_as

    ORDER }o--|| DELIVERY_METHOD : uses


## ▶️ Getting Started

### Prerequisites

- .NET SDK 7+
- SQL Server
- Visual Studio or VS Code

---

### Setup

#### Clone the repository

```bash
git clone https://github.com/ziadr14/ECcommerce.Backend.git

