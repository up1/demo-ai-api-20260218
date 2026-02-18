# System Patterns

## Architecture
```
Client → Minimal API Endpoints → Services → EF Core DbContext → PostgreSQL
```

## Key Technical Decisions
- **Minimal API** over Controller-based for lightweight, focused endpoints
- **Entity Framework Core + Npgsql** for PostgreSQL access
- **Service layer** (`OrderService`) separates business logic from endpoint routing
- **Extension method** pattern for endpoint mapping (`MapOrderEndpoints()`)

## Design Patterns
| Pattern              | Usage                                                    |
|----------------------|----------------------------------------------------------|
| Repository (implicit)| EF Core DbContext acts as repository                     |
| DTO pattern          | Request/Response models decoupled from entities          |
| HATEOAS              | Self links in response (`LinkResponse`)                  |
| Dependency Injection | Services and DbContext registered in DI container        |
| Integration Testing  | `WebApplicationFactory<Program>` with InMemory DB swap   |

## Component Relationships
- `Program.cs` → registers DI services, maps endpoints
- `Endpoints/OrderEndpoints.cs` → route group `/api/v1/orders`, calls `OrderService`
- `Services/OrderService.cs` → business logic, uses `AppDbContext`
- `Data/AppDbContext.cs` → EF Core context with PostgreSQL table/column mappings (snake_case)
- `Entities/` → `Customer`, `Product`, `Order`, `OrderLineItem`
- `Models/` → `CreateOrderRequest`, `CreateOrderResponse` (DTOs)

## Critical Implementation Paths
1. **Create Order:** POST → `OrderEndpoints` → `OrderService.CreateOrderAsync()` → `AppDbContext.SaveChanges()` → 201 response
2. **Test:** `WebApplicationFactory` replaces Npgsql with InMemory → seeds Customer + Products → sends HTTP POST → asserts 201 + body
