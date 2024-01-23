

namespace RestaurantManagement_Repository.Model.Entity
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int MenuId { get; set; }

        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public bool IsActive { get; set; }

        public virtual Order? Order { get; set; }

       public virtual Menu Menu { get; set; }

        
    }
}
