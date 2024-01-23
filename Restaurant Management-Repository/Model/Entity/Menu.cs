
namespace RestaurantManagement_Repository.Model.Entity
{
    public class Menu
    {
        public int    MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


       public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

      

    }
}
