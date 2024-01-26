
namespace RestaurantManagement.Model.Entity
{
    public class Menu
    {
        public int    MenuId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }


       public required decimal Price { get; set; }

        public required bool IsActive { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }

      

    }
}
