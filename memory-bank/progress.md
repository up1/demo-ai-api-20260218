# Progress

## What Works
- ✅ POST `/api/v1/orders` returns 201 with order details (success case)
- ✅ Entity Framework Core + PostgreSQL data access layer
- ✅ Entities: Customer, Product, Order, OrderLineItem
- ✅ DTOs: CreateOrderRequest/Response with nested models
- ✅ OrderService with business logic (totalPrice calculation, status = "pending")
- ✅ HATEOAS self links in response
- ✅ Integration test infrastructure (`WebApplicationFactory` + InMemory DB)
- ✅ Test seeding (Customer, Products)
- ✅ One passing integration test: `CreateOrder_WithValidRequest_Returns201WithOrderResponse`
- ✅ OpenAPI enabled in Development

## What's Left to Build
- ❌ Error handling (400, 401, 403, 404, 429, 500)
- ❌ Model validation (Data Annotations / FluentValidation)
- ❌ Error handling middleware
- ❌ Authentication (JWT Bearer)
- ❌ API versioning
- ❌ Logging (Serilog)
- ❌ Expanded test coverage (unit tests, edge cases)
- ❌ GET, PUT, DELETE endpoints for Orders
- ❌ Caching and performance optimization

## Known Issues
- `UnitTest1.cs` is a placeholder with no real test logic
- No custom error responses — unhandled exceptions return default 500

## Evolution of Decisions
1. Started with Minimal API pattern (not Controllers)
2. Added EF Core + Npgsql for PostgreSQL
3. Created OrderService for separation of concerns
4. Set up integration tests with InMemory DB swap
5. Next: error handling, validation, authentication
