using Mapster;
using Microsoft.EntityFrameworkCore;
using OutboxPatternWebApi.BakgroundService;
using OutboxPatternWebApi.Context;
using OutboxPatternWebApi.Dtos;
using OutboxPatternWebApi.Models;
using TS.Result;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddFluentEmail("alicanyucel@yucelyazilim.com").AddSmtpSender("localhost", 25);
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<OrderBackgroundService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
// create order
app.MapPost("api/orders/create", async (CreateOrderDto request, ApplicationDbContext dbcontext, CancellationToken cancelllationToken) =>
{
    Order order = request.Adapt<Order>();
    dbcontext.Add(order);
    OrderOutBox orderOutBox = new()
    {
        OrderId = order.Id,
        CreatedDate = DateTimeOffset.Now,
    };
    dbcontext.Add(orderOutBox);
    await dbcontext.SaveChangesAsync(cancelllationToken);
    return Results.Ok(Result<string>.Succeed("sipari� olu�turuldu"));
}).Produces<Result<string>>();
// getall
app.MapGet("api/orders/getAll", async (ApplicationDbContext dbcontext, CancellationToken cancelllationToken) =>
{
    List<Order> orders = await dbcontext.Orders.ToListAsync(cancelllationToken);
    return Results.Ok(orders);
}).Produces<List<Order>>();
app.MapControllers();
app.Run();
