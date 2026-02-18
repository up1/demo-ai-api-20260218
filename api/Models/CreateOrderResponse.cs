namespace api.Models;

public class CreateOrderResponse
{
    public required string OrderId { get; set; }
    public required string CustomerId { get; set; }
    public required string Status { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public required List<LineItemResponse> LineItems { get; set; }
    public required List<LinkResponse> Links { get; set; }
}

public class LineItemResponse
{
    public required string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class LinkResponse
{
    public required string Rel { get; set; }
    public required string Href { get; set; }
}
