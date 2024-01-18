

namespace RestaurantManagement_Repository.Model.Entity
{
    public class Order
    {
        public int OrderId { get; set; }

        //public int TableNumber { get; set; }
       
        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<EmployeeOrder> EmployeeOrder { get; set; }
        public virtual Table Table { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
