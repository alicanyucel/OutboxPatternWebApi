namespace OutboxPatternWebApi.Models;

public sealed class Order
{
    public Order()
    {
        Id=Guid.NewGuid();
    }
    public DateTimeOffset CreateDate { get; set; }
    public Guid Id { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public decimal Price { get; set; }=default!;
    public string CustomerEmail { get; set; } = default!;

}
