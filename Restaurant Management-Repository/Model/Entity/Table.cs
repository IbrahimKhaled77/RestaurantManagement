

namespace RestaurantManagement_Repository.Model.Entity
{
    public class Table
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        
        public virtual ICollection<Order> Order { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
