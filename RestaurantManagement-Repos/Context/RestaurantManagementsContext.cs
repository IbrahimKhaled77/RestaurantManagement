using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.Model.EntityConfiguration;

namespace RestaurantManagement.Context
{
    public class RestaurantManagemenstContext : DbContext
    {
        public RestaurantManagemenstContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            //Here the model verification is reversed on Database
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeOrderConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());


        }

        //The model is reflected in tabular form for Database
        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<Employee> Employee { get; set; }

       public virtual DbSet<EmployeeOrder> EmployeeOrder { get; set; }

        public virtual DbSet<Menu> Menu { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<OrderItem> OrderItem { get; set; }

        public virtual DbSet<Table> Table { get; set; }


    }
}
