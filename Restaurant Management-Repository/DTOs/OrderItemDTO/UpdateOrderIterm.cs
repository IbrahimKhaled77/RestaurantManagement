


namespace RestaurantManagement.DTOs.OrderItemDTO
{
    public class UpdateOrderIterm
    {
        
        public required int OrderItemId { get; set; }
        public required int MenuId { get; set; }
               
        public required int OrderId { get; set; }
        public required int Quantity { get; set; }
               
        public required bool IsAcit { get; set; }

   

    }
}
