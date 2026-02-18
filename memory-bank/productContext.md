# Product Context

## Why This Project Exists
A demonstration/learning project for building production-quality REST APIs with ASP.NET Core 9 Minimal API, showcasing best practices for API design, testing, and tooling with GitHub Copilot.

## Problems It Solves
- Provides a reference implementation for order management via REST API
- Demonstrates proper use of EF Core with PostgreSQL
- Shows how to structure Minimal API projects with separation of concerns

## How It Should Work
1. Client sends a POST request to `/api/v1/orders` with customer, line items, and addresses
2. API validates the request, creates the order in PostgreSQL, and returns 201 with order details
3. Response includes HATEOAS links, calculated `totalPrice`, and order status (`"pending"`)

## User Experience Goals
- Clear, consistent JSON request/response contracts
- Proper HTTP status codes (201 for success, 400/401/403/404/429/500 for errors)
- Self-documenting API via OpenAPI/Swagger
