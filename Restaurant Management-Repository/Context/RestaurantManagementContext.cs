using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.Model.Entity;
using RestaurantManagement_Repository.Model.EntityConfiguration;

namespace RestaurantManagement_Repository.Context
{
    public class RestaurantManagementContext : DbContext
    {
        public RestaurantManagementContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeOrderConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());


        }

        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<Employee> Employee { get; set; }

       public virtual DbSet<EmployeeOrder> EmployeeOrder { get; set; }

        public virtual DbSet<Menu> Menu { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<OrderItem> OrderItem { get; set; }

        public virtual DbSet<Table> Table { get; set; }


    }
}
