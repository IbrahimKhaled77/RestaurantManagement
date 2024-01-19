

using RestaurantManagement_Repository.DTOs.MenuDTO;

namespace RestaurantManagement_Repository.DTOs.OrderItemDTO
{
    public class OrderItemCardDTO
    {
        public int OrderItemId { get; set; }
        public MenuCardDTO Menu { get; set; }
        public int Quantity { get; set; }
    }
}
