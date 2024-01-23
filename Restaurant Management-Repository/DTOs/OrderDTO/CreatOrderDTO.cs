

using RestaurantManagement_Repository.DTOs.CustomerDTO;
using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.DTOs.OrderDTO
{
    public class CreatOrderDTO
    {
        //CreateOrderItemDTO  or OrderItemCardDTO
        public int CustomerId { get; set; }
        public  int TableId { get; set; }    
        public decimal TotalPrice { get; set; }
        public  List<CreateOrderItemDTO> OrderItems { get; set; }

     
       

    }
}
