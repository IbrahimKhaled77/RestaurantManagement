

using RestaurantManagement_Repository.DTOs.OrderDTO;

namespace RestaurantManagement_Repository.DTOs.TableDTO
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
