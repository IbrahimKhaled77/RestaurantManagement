

namespace RestaurantManagement_Repository.DTOs.MenuDTO
{
    public class UpdateMenuDTO
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
