namespace api.Entities;

public class Order
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public required string ShippingStreet { get; set; }
    public required string ShippingCity { get; set; }
    public required string ShippingState { get; set; }
    public required string ShippingZipCode { get; set; }
    public required string BillingStreet { get; set; }
    public required string BillingCity { get; set; }
    public required string BillingState { get; set; }
    public required string BillingZipCode { get; set; }
    public required string Status { get; set; }

    public Customer Customer { get; set; } = null!;
    public List<OrderLineItem> LineItems { get; set; } = [];
}
