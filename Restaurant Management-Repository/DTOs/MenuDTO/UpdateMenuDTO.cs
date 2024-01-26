

namespace RestaurantManagement.DTOs.MenuDTO
{
    public class UpdateMenuDTO
    {
        public required int MenuId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
               
        public required decimal Price { get; set; }
               
        public required bool IsActive { get; set; }
    }
}
