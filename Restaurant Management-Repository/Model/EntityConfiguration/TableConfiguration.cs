

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.Model.EntityConfiguration
{
    internal class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(x => x.TableId);
            builder.Property(x => x.TableId).UseIdentityColumn();

            builder.Property(x => x.TableNumber).IsRequired();
            builder.HasCheckConstraint("TableNumber", "TableNumber >= 0");
        }
    }
}
