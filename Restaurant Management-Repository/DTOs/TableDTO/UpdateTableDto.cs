

namespace RestaurantManagement_Repository.DTOs.TableDTO
{
    public class UpdateTableDto
    {

        public int TableId { get; set; }
        public int TableNumber { get; set; }

        public bool IsActive { get; set; }

    }
}
