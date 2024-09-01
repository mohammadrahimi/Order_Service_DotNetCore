

 
using Order.Domain.OutBox;
using Order.Domain.OutBox.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace CustomerManagement.Persistence.EF.Configurations;

public class OutBoxConfiguration : IEntityTypeConfiguration<OutBox>
{
    public void Configure(EntityTypeBuilder<OutBox> builder)
    {
        ConfigureOutBoxTable(builder);
    }
    private static void ConfigureOutBoxTable(EntityTypeBuilder<OutBox> builder)
    {
        builder.ToTable("OutBoxs");

        builder.HasKey(O => O.Id);

        builder.Property(O => O.Id)
            .ValueGeneratedOnAdd()
            .IsRequired(true);

        builder.Property(O => O.EventType)
            .HasMaxLength(500)
            .IsRequired(true);

        builder.Property(O => O.EventBody)
             .IsRequired()
             .HasMaxLength(4000);

        builder.Property(O => O.CreateDateTime)
             .IsRequired();

        builder.Property(O => O.PublishedAt)
            .ValueGeneratedNever();

        builder.Property(O => O.EventId)
           .HasColumnName("EventId")
           .ValueGeneratedNever()
           .IsRequired(true)
           .HasConversion(
               id => id.Value,
               value => EventID.Create(value));

    }
}
