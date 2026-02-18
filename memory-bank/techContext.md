# Tech Context

## Technologies
| Technology                            | Version  | Purpose                           |
|---------------------------------------|----------|-----------------------------------|
| .NET / ASP.NET Core                   | 9.0      | Runtime & web framework           |
| Entity Framework Core                 | 9.x      | ORM / data access                 |
| Npgsql.EntityFrameworkCore.PostgreSQL | 9.0.4    | PostgreSQL EF Core provider       |
| Microsoft.AspNetCore.OpenApi          | 9.0.0    | OpenAPI documentation             |
| xUnit                                 | 2.9.2    | Test framework                    |
| Microsoft.AspNetCore.Mvc.Testing      | 9.0.0    | Integration test host             |
| Microsoft.EntityFrameworkCore.InMemory| 9.0.0    | In-memory DB for tests            |
| coverlet.collector                    | 6.0.2    | Code coverage                     |

## Development Setup
- **Solution:** `demo01.sln`
- **API project:** `api/api.csproj`
- **Test project:** `tests/tests.csproj` (references `api` project)
- **Run API:** `cd api && dotnet run` (http://localhost:5170)
- **Run tests:** `cd tests && dotnet test`

## Database
- **PostgreSQL** connection: `Host=localhost;Port=5432;Database=orders_db;Username=postgres;Password=postgres`
- Tables: `customers`, `products`, `orders`, `order_line_items`
- Column naming: snake_case (e.g., `unique_id`, `customer_id`, `unit_price`)
- PK strategy: `UseIdentityAlwaysColumn()` (PostgreSQL `GENERATED ALWAYS AS IDENTITY`)
- `unit_price` type: `numeric(10,2)`

## Configuration
- **HTTP Profile:** http://localhost:5170
- **HTTPS Profile:** https://localhost:7141 + http://localhost:5170
- **Environment:** `ASPNETCORE_ENVIRONMENT=Development`
- **OpenAPI:** enabled only in Development (`app.MapOpenApi()`)
- **HTTPS Redirection:** enabled (`app.UseHttpsRedirection()`)

## Technical Constraints
- No authentication/authorization implemented yet
- No custom error handling middleware yet
- No model validation yet
- Success case only (POST 201)

## GitHub Copilot Tooling
- `.github/agents/security-review.agent.md` — OWASP Top 10, Zero Trust security review
- `.github/instructions/dotnet.instructions.md` — ASP.NET Minimal API guidelines
- `.github/prompts/git-add-commit.prompt.md` — Conventional Commits workflow
