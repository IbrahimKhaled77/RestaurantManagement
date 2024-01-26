using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Model.Entity;


namespace RestaurantManagement.Model.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderId).UseIdentityColumn();

            builder.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)").IsRequired();

            builder.HasMany(o => o.EmployeeOrder)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId);

              builder.HasMany(o => o.OrderItems)
             .WithOne(oi => oi.Order)
             .HasForeignKey(oi => oi.OrderId);
        }
    }
}
