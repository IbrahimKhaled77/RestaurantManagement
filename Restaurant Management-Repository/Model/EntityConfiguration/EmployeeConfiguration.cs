
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.Model.EntityConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.EmployeeId);
            builder.Property(x => x.EmployeeId).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
            builder.HasCheckConstraint("Name", "(NOT [Name] like '%[0-9]%' AND NOT [Name] like '%[^A-Za-z ]%')");

            builder.Property(x => x.Email).IsRequired();
            builder.HasCheckConstraint("Email", "([Email] like '%@GMAIL%' OR [Email] like '%@HOTMAIL%' OR [Email] like '%@ICLOUD%')");

            builder.Property(x => x.Password).IsRequired();
            builder.HasCheckConstraint("Password", "(len([Password])=(11) AND [Password] like '%[0-9]%' AND [Password] like '%[A-Za-z]%' AND [Password] like '%[^A-Za-z]%')");


            
            builder.Property(x => x.Position).HasMaxLength(6).IsRequired();
            builder.HasCheckConstraint("Position", "([Position] like '%Waiter%' OR [Position] like '%Chef%' OR [Position] like '%Admin%')");

            builder.HasMany(o => o.EmployeeOrder)
           .WithOne(oi => oi.Employee)
           .HasForeignKey(oi => oi.EmployeeId);
        }
    }
}
