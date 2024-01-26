


namespace RestaurantManagement_Repository.DTOs.OrderItemDTO
{
    public class OrderItemCardDTO
    {

        public int OrderItemId { get; set; }
        public int MenuId { get; set; }

        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
