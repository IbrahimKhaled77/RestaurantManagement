

using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using RestaurantManagement_Repository.DTOs.TableDTO;

namespace RestaurantManagement_Repository.DTOs.OrderDTO
{
    public class OrderCardDTO
    {

        public int OrderId { get; set; }
        public TableCardDTOs TableNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemCardDTO> OrderItems { get; set; }


    }
}
