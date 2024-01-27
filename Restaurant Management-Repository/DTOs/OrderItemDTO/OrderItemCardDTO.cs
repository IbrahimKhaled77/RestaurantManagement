


namespace RestaurantManagement.DTOs.OrderItemDTO
{
    public class OrderItemCardDTO
    {

        public required  int OrderItemId { get; set; }
        public required int MenuId { get; set; }
              
        public string MenuName { get; set; }

        public decimal price { get; set; }
        public required int OrderId { get; set; }

       
        public required int Quantity { get; set; }
        public required bool IsActive { get; set; }
    }
}
