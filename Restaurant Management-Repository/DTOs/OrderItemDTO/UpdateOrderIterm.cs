
using RestaurantManagement_Repository.DTOs.MenuDTO;

namespace RestaurantManagement_Repository.DTOs.OrderItemDTO
{
    public class UpdateOrderIterm
    {
        //UpdateMenuDTO or 
        public int OrderItemId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }

      //  public int OrderId { get; set; }****

    }
}
