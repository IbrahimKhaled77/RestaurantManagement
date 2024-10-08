﻿

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Model.EntityConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.MenuId);
            builder.Property(x => x.MenuId).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired();
            builder.HasCheckConstraint("Name", "(NOT [Name] like '%[0-9]%' AND NOT [Name] like '%[^A-Za-z ]%')");

            builder.Property(x => x.Description).IsRequired();
            builder.HasCheckConstraint("Description", "(NOT [Description] like '%[0-9]%' AND NOT [Description] like '%[^A-Za-z ]%')");


          
            builder.Property(e => e.Price).HasColumnType("decimal(18, 2)") .IsRequired();

            builder.HasMany(i => i.OrderItems) .WithOne(oi => oi.Menu).HasForeignKey(oi => oi.MenuId);

        }
    }
}
