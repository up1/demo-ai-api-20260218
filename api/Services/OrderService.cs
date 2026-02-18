using api.Data;
using api.Entities;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class OrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, HttpRequest httpRequest)
    {
        // Look up customer by unique ID
        var customer = await _db.Customers
            .FirstAsync(c => c.UniqueId == request.CustomerId);

        // Look up all referenced products
        var productUniqueIds = request.LineItems.Select(li => li.ProductId).ToList();
        var products = await _db.Products
            .Where(p => productUniqueIds.Contains(p.UniqueId))
            .ToDictionaryAsync(p => p.UniqueId, p => p.Id);

        // Build the order entity
        var order = new Order
        {
            CustomerId = customer.Id,
            OrderDate = request.OrderDate,
            Status = "pending",
            ShippingStreet = request.ShippingAddress.Street,
            ShippingCity = request.ShippingAddress.City,
            ShippingState = request.ShippingAddress.State,
            ShippingZipCode = request.ShippingAddress.ZipCode,
            BillingStreet = request.BillingAddress.Street,
            BillingCity = request.BillingAddress.City,
            BillingState = request.BillingAddress.State,
            BillingZipCode = request.BillingAddress.ZipCode,
            LineItems = request.LineItems.Select(li => new OrderLineItem
            {
                ProductId = products[li.ProductId],
                Quantity = li.Quantity,
                UnitPrice = li.UnitPrice
            }).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        // Calculate total price
        var totalPrice = order.LineItems.Sum(li => li.Quantity * li.UnitPrice);

        // Build the base URL for HATEOAS links
        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        return new CreateOrderResponse
        {
            OrderId = order.Id.ToString(),
            CustomerId = request.CustomerId,
            Status = order.Status,
            TotalPrice = totalPrice,
            OrderDate = order.OrderDate,
            LineItems = order.LineItems.Select(li => new LineItemResponse
            {
                ProductId = request.LineItems
                    .First(r => products[r.ProductId] == li.ProductId).ProductId,
                Quantity = li.Quantity,
                UnitPrice = li.UnitPrice
            }).ToList(),
            Links =
            [
                new LinkResponse
                {
                    Rel = "self",
                    Href = $"{baseUrl}/api/v1/orders/{order.Id}"
                }
            ]
        };
    }
}
