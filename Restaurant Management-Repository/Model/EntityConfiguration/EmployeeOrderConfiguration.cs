using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Model.Entity;


namespace RestaurantManagement.Model.EntityConfiguration
{
    public class EmployeeOrderConfiguration : IEntityTypeConfiguration<EmployeeOrder>
    {
        public void Configure(EntityTypeBuilder<EmployeeOrder> builder)
        {
            builder.HasKey(x => x.EmployeeOrderId);
            builder.Property(x => x.EmployeeOrderId).UseIdentityColumn();
        }
    }
}
