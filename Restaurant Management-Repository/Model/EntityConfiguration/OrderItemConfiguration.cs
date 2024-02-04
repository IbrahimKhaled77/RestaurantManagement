using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Model.Entity;


namespace RestaurantManagement.Model.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemId);
            builder.Property(x => x.OrderItemId).UseIdentityColumn();

            //Quantity or number of one product. How much should one order from it and be positive 
            builder.Property(x => x.Quantity).IsRequired();
            builder.HasCheckConstraint("Quantity", "Quantity >= 1");
           

        }
    }
}
