namespace api.Models;

public class CreateOrderRequest
{
    public required string CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public required List<LineItemRequest> LineItems { get; set; }
    public required AddressRequest ShippingAddress { get; set; }
    public required AddressRequest BillingAddress { get; set; }
}

public class LineItemRequest
{
    public required string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class AddressRequest
{
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
}
