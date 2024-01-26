
namespace RestaurantManagement.Model.Entity
{
    public class Table
    {
        
        public  int TableId { get; set; }
        public int TableNumber { get; set; }
        public bool IsActive { get; set; }

        public bool IsActiveOrder { get; set; }
       
        public virtual ICollection<Order> Order { get; set; }
        
 
    }
}
