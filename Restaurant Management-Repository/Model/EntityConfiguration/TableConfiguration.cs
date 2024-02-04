

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Model.EntityConfiguration
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(x => x.TableId);
            builder.Property(x => x.TableId).UseIdentityColumn();

            builder.Property(x => x.TableNumber).IsRequired();

            builder.HasIndex(x => x.TableNumber).IsUnique();
            builder.HasCheckConstraint("TableNumber", "TableNumber >= 1");
        }
    }
}
