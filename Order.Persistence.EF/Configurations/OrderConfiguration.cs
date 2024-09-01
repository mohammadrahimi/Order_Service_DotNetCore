


using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Order.State;
using Order.Domain.Order.ValueObjects;

namespace Order.Persistence.EF.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order.Domain.Order.Order>
{
    public void Configure(EntityTypeBuilder<Order.Domain.Order.Order> builder)
    {
        ConfigureUserTable(builder);
        ConfigureOrderItemTable(builder);
    }

    private static void ConfigureUserTable(EntityTypeBuilder<Order.Domain.Order.Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(O => O.Id);

        builder.Property(O => O.Id)
            .ValueGeneratedNever()
            .IsRequired(true)
            .HasConversion(
            id => id.Value,
            value => OrderId.Create(value));

        builder.Property(O => O.CustomerName)
            .HasMaxLength(150);

        builder.Property(O => O.CustomerID)
           .HasColumnName("CustomerID")
           .ValueGeneratedNever()
           .IsRequired(true)
           .HasConversion(
               id => id.Value,
               value => CustomerID.Create(value));

        builder.Property(O => O.OrderDateTime)
            .IsRequired();


        builder.OwnsOne(O => O.TotalPrice, o =>
        {
            o.Property(o => o.Amount).IsRequired(true);
            o.Property(o => o.Currency).HasMaxLength(50);
        });

        builder.Property(p => p.State)
               .HasConversion(
               p => p.GetType().Name,
               p => GetOrderState(p)
         );
    }

    private static void ConfigureOrderItemTable(EntityTypeBuilder<Order.Domain.Order.Order> builder)
    {
        builder.OwnsMany(O => O.OrderItems, oi =>
        {
            oi.ToTable("OrderItems");

            oi.WithOwner().HasForeignKey("OrderId");

            oi.HasKey(oi => oi.Id);


            oi.Property(oi => oi.Id)
            .HasColumnName("OrderItemId")
            .ValueGeneratedNever()
            .IsRequired(true)
            .HasConversion(
                id => id.Value,
                value => OrderItemId.Create(value));

            oi.Property(P => P.ProductId)
            .HasColumnName("ProductId")
            .ValueGeneratedNever()
            .IsRequired(true)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

            oi.Property(oi => oi.Quantity);

            oi.OwnsOne(x => x.Price,
               oiP =>
               {
                   oiP.Property(x => x.Currency)
                       .HasMaxLength(10)
                       .IsRequired(true);
                   oiP.Property(x => x.Amount)
                   .IsRequired(true);
               });

        });

    }
    public static OrderState GetOrderState(string state)
    {
        return state switch
        {
            nameof(PendingState) => new PendingState(),
            nameof(RejectState) => new RejectState(),
            nameof(PaiedState) => new PaiedState(),
            nameof(DeliverdState) => new DeliverdState(),
            _ => throw new NotImplementedException(),
        };
    }

}
