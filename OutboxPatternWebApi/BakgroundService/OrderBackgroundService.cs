
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
            var outboxes = await dbcontext.orderOutBoxes.Where(p => p.IsCompleted).OrderBy(p => p.CreatedDate).ToListAsync(stoppingToken);

            foreach (var item in outboxes)
            {
            }
        }
    }
}