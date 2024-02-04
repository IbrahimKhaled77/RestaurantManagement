


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Model.Entity;





namespace RestaurantManagement.Model.EntityConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();
            builder.HasCheckConstraint("Name", "(NOT [Name] like '%[0-9]%' AND NOT [Name] like '%[^A-Za-z ]%')");


            builder.Property(x => x.Email).IsRequired();
            builder.HasCheckConstraint("Email", "([Email] like '%@GMAIL%' OR [Email] like '%@HOTMAIL%' OR [Email] like '%@ICLOUD%')");


            builder.Property(x => x.Password).IsRequired();
            builder.HasCheckConstraint("Password", "(len([Password])>=(11) AND [Password] like '%[0-9]%' AND [Password] like '%[A-Za-z]%' AND [Password] like '%[^A-Za-z]%')");
            

            //Be the first phone number + then enter 12 numbers
            builder.Property(x => x.PhoneNumber).HasMaxLength(13).IsRequired();
            builder.HasCheckConstraint("PhoneNumber", "([PhoneNumber] like '+%' AND len([PhoneNumber])=(13) AND [PhoneNumber] like '%[0-9]%')");
           
        }


    }
}
