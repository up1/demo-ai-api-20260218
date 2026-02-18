# Active Context

## Current Work Focus
- POST `/api/v1/orders` endpoint is implemented (success case, 201)
- Integration test exists in `tests/CreateOrderApiTest.cs` using `WebApplicationFactory` + InMemory EF Core

## Recent Changes
- Created full order creation flow: Endpoint → OrderService → AppDbContext
- Entities: Customer, Product, Order, OrderLineItem
- DTOs: CreateOrderRequest/Response with nested LineItem, Address models
- Test infrastructure with InMemory database replacing Npgsql for testing

## Next Steps
- [ ] Add error handling (400, 401, 403, 404, 429, 500)
- [ ] Add model validation (Data Annotations / FluentValidation)
- [ ] Add error handling middleware
- [ ] Add authentication (JWT Bearer)
- [ ] Add API versioning
- [ ] Add logging (Serilog)
- [ ] Add unit tests and expand integration tests
- [ ] Add caching and performance optimization
- [ ] Add GET, PUT, DELETE endpoints for Orders

## Active Decisions
- Using Minimal API (not Controllers) for conciseness
- PostgreSQL for production, InMemory for tests
- HATEOAS links included in responses
- Order status defaults to `"pending"`
- No authentication/authorization yet
- No custom error handling or validation yet (success case only)
