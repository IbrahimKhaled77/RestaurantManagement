
namespace RestaurantManagement.Model.Entity
{
    public class Order
    {
        public int OrderId { get; set; }

       
        public int TableNumber { get; set; }
       
        public decimal TotalPrice { get; set; }

        public bool IsActive { get; set; }

        public virtual List<OrderItem>? OrderItems { get; set; }
        public virtual List<EmployeeOrder>? EmployeeOrder { get; set; }
        public virtual Table? Table { get; set; }

        public virtual Customer? Customer { get; set; }

      
    }
}
