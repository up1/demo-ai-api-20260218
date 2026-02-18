using api.Models;
using api.Services;

namespace api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/orders");

        group.MapPost("/", async (CreateOrderRequest request, OrderService orderService, HttpRequest httpRequest) =>
        {
            var response = await orderService.CreateOrderAsync(request, httpRequest);
            return Results.Created($"/api/v1/orders/{response.OrderId}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .WithOpenApi();
    }
}
