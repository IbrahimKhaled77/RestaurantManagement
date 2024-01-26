



using RestaurantManagement.DTOs.EmployeeOrderDTO;

namespace RestaurantManagement.DTOs.EmployeeDTO
{
    public class EmployeeCardDTO
    {
        public required int EmployeeId { get; set; }
        public required string Name { get; set; }
        public required  string Email { get; set; }
        public required  string Position { get; set; }

        public required bool IsActive { get; set; }

        public List<EmployeeOrderCardDTo>? EmployeeOrders { get; set; }
    }
}
