namespace OutboxPatternWebApi.Dtos;

public sealed record CreateOrderDto(string ProductName,DateTimeOffset CratedDate,int Quantity,decimal Price,string CustomerEmail);