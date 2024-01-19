

using RestaurantManagement_Repository.DTOs.OrderItemDTO;

namespace RestaurantManagement_Repository.DTOs.OrderDTO
{
    public class CreatOrderDTO
    {
        //CreateOrderItemDTO  or OrderItemCardDTO
        public int CustomerId { get; set; }
        public int TableNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; }

     
       

    }
}
