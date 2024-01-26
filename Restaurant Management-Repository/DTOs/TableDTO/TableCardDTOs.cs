

using RestaurantManagement.DTOs.OrderDTO;

namespace RestaurantManagement.DTOs.TableDTO
{
    public class TableCardDTOs
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }

      


        public List<OrderCardDTO>? Orders { get; set; }

        public bool IsActive { get; set; }

        public bool IsActiveOrder { get; set; }
    }
}
