using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement_Repository.Model.Entity;


namespace RestaurantManagement_Repository.Model.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemId);
            builder.Property(x => x.OrderItemId).UseIdentityColumn();


            builder.Property(x => x.Quantity).IsRequired();
            builder.HasCheckConstraint("Quantity", "Quantity >= 0");
           

        }
    }
}
