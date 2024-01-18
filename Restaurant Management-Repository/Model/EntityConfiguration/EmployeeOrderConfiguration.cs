using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement_Repository.Model.Entity;


namespace RestaurantManagement_Repository.Model.EntityConfiguration
{
    internal class EmployeeOrderConfiguration : IEntityTypeConfiguration<EmployeeOrder>
    {
        public void Configure(EntityTypeBuilder<EmployeeOrder> builder)
        {
            builder.HasKey(x => x.EmployeeOrderId);
            builder.Property(x => x.EmployeeOrderId).UseIdentityColumn();
        }
    }
}
