



namespace RestaurantManagement.DTOs.OrderItemDTO
{
    public class CreateOrderItemDTO
    {

     
        public required  int MenuId { get; set; }
               
        public required int OrderId { get; set; }
        public required int Quantity { get; set; }
               
        public required bool IsActive { get; set; }
        


    }
}
