using System.Net;
using System.Net.Http.Json;
using api.Data;
using api.Entities;
using api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace tests;

public class CreateOrderApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public CreateOrderApiTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove all EF Core / AppDbContext registrations (including Npgsql provider)
                for (var i = services.Count - 1; i >= 0; i--)
                {
                    var serviceType = services[i].ServiceType;
                    if (serviceType == typeof(AppDbContext)
                        || serviceType == typeof(DbContextOptions<AppDbContext>)
                        || serviceType == typeof(DbContextOptions))
                    {
                        services.RemoveAt(i);
                    }
                }

                // Register AppDbContext with InMemory database via explicit factory
                // This avoids dual-provider conflicts between Npgsql and InMemory
                services.AddScoped<AppDbContext>(_ =>
                {
                    var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase("TestOrdersDb")
                        .Options;
                    return new AppDbContext(options);
                });
            });
        });

        _client = _factory.CreateClient();
    }

    private static void SeedDatabase(AppDbContext db)
    {
        db.Database.EnsureCreated();

        // Only seed if empty (avoid duplicate key on re-runs within same fixture)
        if (!db.Customers.Any())
        {
            db.Customers.Add(new Customer { Id = 1, UniqueId = "unique-customer-id-123" });
            db.Products.Add(new Product { Id = 1, UniqueId = "product-id-abc" });
            db.Products.Add(new Product { Id = 2, UniqueId = "product-id-xyz" });
            db.SaveChanges();
        }
    }

    [Fact]
    public async Task CreateOrder_WithValidRequest_Returns201WithOrderResponse()
    {
        // Arrange — seed test data
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            SeedDatabase(db);
        }

        var request = new CreateOrderRequest
        {
            CustomerId = "unique-customer-id-123",
            OrderDate = DateTime.Parse("2025-01-20T23:24:00Z").ToUniversalTime(),
            LineItems =
            [
                new LineItemRequest { ProductId = "product-id-abc", Quantity = 2, UnitPrice = 10.50m },
                new LineItemRequest { ProductId = "product-id-xyz", Quantity = 1, UnitPrice = 25.00m }
            ],
            ShippingAddress = new AddressRequest
            {
                Street = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            },
            BillingAddress = new AddressRequest
            {
                Street = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/orders", request);

        // Assert — HTTP 201 Created
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // Assert — response body
        var body = await response.Content.ReadFromJsonAsync<CreateOrderResponse>();
        Assert.NotNull(body);
        Assert.Equal("unique-customer-id-123", body.CustomerId);
        Assert.Equal("pending", body.Status);
        Assert.Equal(46.00m, body.TotalPrice);
        Assert.Equal(DateTime.Parse("2025-01-20T23:24:00Z").ToUniversalTime(), body.OrderDate);

        // Assert — line items
        Assert.Equal(2, body.LineItems.Count);
        Assert.Contains(body.LineItems,
            li => li.ProductId == "product-id-abc" && li.Quantity == 2 && li.UnitPrice == 10.50m);
        Assert.Contains(body.LineItems,
            li => li.ProductId == "product-id-xyz" && li.Quantity == 1 && li.UnitPrice == 25.00m);

        // Assert — HATEOAS links
        Assert.Single(body.Links);
        Assert.Equal("self", body.Links[0].Rel);
        Assert.Contains($"/api/v1/orders/{body.OrderId}", body.Links[0].Href);

        // Assert — Location header
        Assert.NotNull(response.Headers.Location);
        Assert.Contains($"/api/v1/orders/{body.OrderId}", response.Headers.Location.ToString());
    }
}
