
using Microsoft.AspNetCore.Mvc;
using OutboxPatternWebApi.Models;

namespace OutboxPatternWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        List<Order> orders = new List<Order>
    {
    new Order {  CustomerEmail = "yucelalican@hotmail.com", ProductName = "kalem",Price=5, Quantity = 1 }     
    };
        return Ok(orders);
    }
}
