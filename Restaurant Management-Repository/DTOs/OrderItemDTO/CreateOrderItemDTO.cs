

using RestaurantManagement_Repository.DTOs.MenuDTO;

namespace RestaurantManagement_Repository.DTOs.OrderItemDTO
{
    public class CreateOrderItemDTO
    {

     
        
        public int MenuId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public bool IsActive { get; set; }
        //orderId*


    }
}
