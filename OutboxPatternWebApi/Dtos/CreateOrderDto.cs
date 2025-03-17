namespace OutboxPatternWebApi.Dtos;

public sealed record CreateOrderDto(string ProductName,DateTimeOffset CreatedDate,int Quantity,decimal Price,string CustomerEmail);