namespace OutboxPatternWebApi.Models;

public sealed class OrderOutBox
{
    public OrderOutBox()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public bool IsCompleted {  get; set; }
    public bool IsFailed {  get; set; }
    public DateTimeOffset? CompletaDate { get; set; }
    public string FailMessage { get; set; } = string.Empty;
}

