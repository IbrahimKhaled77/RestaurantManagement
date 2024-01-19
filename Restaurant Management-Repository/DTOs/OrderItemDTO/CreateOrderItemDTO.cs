

using RestaurantManagement_Repository.DTOs.MenuDTO;

namespace RestaurantManagement_Repository.DTOs.OrderItemDTO
{
    public class CreateOrderItemDTO
    {

     
        public MenuCardDTO MenuId { get; set; }
        public int Quantity { get; set; }

        //orderId*
        

    }
}
