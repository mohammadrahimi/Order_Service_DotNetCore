


using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Order.Domain.OutBox;

namespace Order.Persistence.EF.Context;

public class OrderDbContext : ApplicationDbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

    public DbSet<Order.Domain.Order.Order> Orders { get; set; }
    public DbSet<OutBox> outBoxes { get; set; }
}
