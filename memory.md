# Project Memory

## Overview
- **Project Name:** api
- **Type:** ASP.NET Core 9 Minimal API (Web API)
- **Target Framework:** .NET 9.0 (`net9.0`)
- **API Style:** Minimal API (ไม่ใช่ Controller-based)
- **Database:** PostgreSQL (via Entity Framework Core + Npgsql)

## Project Structure
```
.github/
  copilot-instructions.md       # คำแนะนำสำหรับ Copilot เกี่ยวกับ ASP.NET REST API
features/
  1.md                          # Spec: REST API Create a new order
api/
  Program.cs                    # Entry point — DI, middleware, endpoint mapping
  api.csproj                    # ไฟล์โปรเจกต์ .NET
  api.http                      # ไฟล์ทดสอบ HTTP requests (POST create order)
  appsettings.json              # การตั้งค่าหลัก + ConnectionStrings
  appsettings.Development.json  # การตั้งค่าสำหรับ Development
  Properties/
    launchSettings.json          # การตั้งค่า launch profiles
  Models/
    CreateOrderRequest.cs        # Request DTOs (CreateOrderRequest, LineItemRequest, AddressRequest)
    CreateOrderResponse.cs       # Response DTOs (CreateOrderResponse, LineItemResponse, LinkResponse)
  Entities/
    Customer.cs                  # Customer entity (id, unique_id)
    Product.cs                   # Product entity (id, unique_id)
    Order.cs                     # Order entity (addresses, status, customer FK)
    OrderLineItem.cs             # OrderLineItem entity (product FK, quantity, unit_price)
  Data/
    AppDbContext.cs               # EF Core DbContext — PostgreSQL table/column mappings
  Services/
    OrderService.cs              # Business logic สำหรับสร้าง Order
  Endpoints/
    OrderEndpoints.cs            # Minimal API route group: /api/v1/orders
```

## Dependencies
- **Microsoft.AspNetCore.OpenApi** v9.0.0 — สำหรับสร้าง OpenAPI documentation
- **Npgsql.EntityFrameworkCore.PostgreSQL** v9.0.4 — EF Core provider สำหรับ PostgreSQL

## Endpoints
| Method | Path                | Status  | Description                        |
|--------|---------------------|---------|------------------------------------|
| POST   | `/api/v1/orders`    | 201     | สร้าง order ใหม่ (success case)      |

## Database Schema (PostgreSQL)
| Table              | Description                                    |
|--------------------|------------------------------------------------|
| `customers`        | id (PK, identity), unique_id (unique, not null)|
| `products`         | id (PK, identity), unique_id (unique, not null)|
| `orders`           | id (PK, identity), customer_id (FK), order_date, shipping/billing address fields, status |
| `order_line_items` | id (PK, identity), order_id (FK), product_id (FK), quantity, unit_price numeric(10,2) |

## Models

### Request DTOs
- **CreateOrderRequest** — customerId, orderDate, lineItems[], shippingAddress, billingAddress
- **LineItemRequest** — productId, quantity, unitPrice
- **AddressRequest** — street, city, state, zipCode

### Response DTOs
- **CreateOrderResponse** — orderId, customerId, status, totalPrice, orderDate, lineItems[], links[]
- **LineItemResponse** — productId, quantity, unitPrice
- **LinkResponse** — rel, href (HATEOAS)

### Entities
- **Customer** — Id, UniqueId
- **Product** — Id, UniqueId
- **Order** — Id, CustomerId, OrderDate, Shipping/Billing address fields, Status, navigation: Customer, LineItems
- **OrderLineItem** — Id, OrderId, ProductId, Quantity, UnitPrice, navigation: Order, Product

## Configuration
- **HTTP Profile:** `http://localhost:5170`
- **HTTPS Profile:** `https://localhost:7141` + `http://localhost:5170`
- **Environment:** `ASPNETCORE_ENVIRONMENT=Development`
- **OpenAPI:** เปิดใช้งานเฉพาะใน Development (`app.MapOpenApi()`)
- **HTTPS Redirection:** เปิดใช้งาน (`app.UseHttpsRedirection()`)
- **ConnectionStrings.DefaultConnection:** `Host=localhost;Port=5432;Database=orders_db;Username=postgres;Password=postgres`

## Key Decisions
- ใช้ **Minimal API** แทน Controller-based API เพื่อความกระชับ
- ใช้ **Entity Framework Core + Npgsql** สำหรับ PostgreSQL data access
- **OrderService** แยก business logic ออกจาก endpoint (separation of concerns)
- Endpoint mapping ผ่าน extension method `MapOrderEndpoints()` ใน `Endpoints/` folder
- Response มี **HATEOAS links** (self link)
- **totalPrice** คำนวณจาก sum ของ quantity * unitPrice ของแต่ละ line item
- Order status เริ่มต้นเป็น `"pending"`
- ยังไม่มี authentication/authorization
- ยังไม่มี custom error handling หรือ validation (success case only)
- ยังไม่มี unit tests

## Next Steps (ตาม copilot-instructions.md)
- [x] เพิ่ม CRUD endpoints สำหรับ resource ใหม่ (POST Create Order)
- [x] เพิ่ม Data Access Layer (Entity Framework Core + PostgreSQL)
- [ ] เพิ่ม error handling (400, 401, 403, 404, 429, 500) สำหรับ Create Order
- [ ] เพิ่ม Model Validation (Data Annotations / FluentValidation)
- [ ] เพิ่ม Error Handling Middleware
- [ ] เพิ่ม Authentication (JWT Bearer)
- [ ] เพิ่ม API Versioning
- [ ] เพิ่ม Logging (Serilog)
- [ ] เพิ่ม Unit Tests และ Integration Tests
- [ ] เพิ่ม Caching และ Performance Optimization
- [ ] เพิ่ม GET, PUT, DELETE endpoints สำหรับ Orders
