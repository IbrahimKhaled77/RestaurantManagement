


using RestaurantManagement.DTOs.OrderItemDTO;


namespace RestaurantManagement.DTOs.OrderDTO
{
    public class CreatOrderDTO
    {
       
        public required  int CustomerId { get; set; }
        public required   int TableId { get; set; }    
        public required  decimal TotalPrice { get; set; }
        public required List<CreateOrderItemDTO> OrderItems { get; set; }

     
       

    }
}
