using Microsoft.EntityFrameworkCore;
using OutboxPatternWebApi.Models;

namespace OutboxPatternWebApi.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }
        public DbSet<Order> Orders { get; set; }
    }
}
