

using RestaurantManagement_Repository.DTOs.OrderDTO;

namespace RestaurantManagement_Repository.DTOs.TableDTO
{
    public class TableCardDTOs
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }

        //مش مهمه هون هي مهمه بوقت الفاتوره 
        //عادي اذ سوينها اذ صار في مشكله بنشيلها لقدام 
        public List<OrderCardDTO> Orders { get; set; }

        public bool IsActive { get; set; }
    }
}
