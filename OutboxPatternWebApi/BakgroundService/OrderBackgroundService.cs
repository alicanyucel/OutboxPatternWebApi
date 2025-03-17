
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using OutboxPatternWebApi.Context;

namespace OutboxPatternWebApi.BakgroundService;

public sealed class OrderBackgroundService(IServiceProvider serviceprovider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scoped = serviceprovider.CreateScope())
        {
            var dbcontext = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var fluentmail = scoped.ServiceProvider.GetRequiredService<IFluentEmail>();
            while (!stoppingToken.IsCancellationRequested)
            {
                var outboxes = await dbcontext.orderOutBoxes.Where(p => !p.IsCompleted).OrderBy(p => p.CreatedDate).ToListAsync(stoppingToken);
                foreach (var item in outboxes)
                {
                    try
                    {
                        var order = await dbcontext.Orders.FirstAsync(p => p.Id == item.OrderId, stoppingToken);
                        string body = @"
                       <h1>sipariş durumu:<b>Başarılı</b>
                       <p>Siparis başarıyla oluşturuldu</p>
                       <p>Süreç hakkında bilgilendirme maili göndereceğiz</p>";
                        body = body.Replace("[productName]", order.ProductName);
                       var response=  await fluentmail.To(order.CustomerEmail).Subject("olusturulan sipairş").Body(body).SendAsync(stoppingToken);
                        item.IsCompleted = true;
                        item.CreatedDate = DateTime.Now;
                        if(response.Successful)
                        {
                            item.IsFailed=true;
                            item.FailMessage = response.ErrorMessages.FirstOrDefault();
                        }
                    }

                    catch (Exception ex)
                    {
                        item.IsFailed = true;
                        item.IsCompleted = true;
                        item.CreatedDate = DateTime.Now;
                        item.FailMessage = ex.Message;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
                await dbcontext.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromMinutes(10));
            }
        }
    }
}