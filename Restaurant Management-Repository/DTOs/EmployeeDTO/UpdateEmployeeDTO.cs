

namespace RestaurantManagement.DTOs.EmployeeDTO
{
    public class UpdateEmployeeDTO
    {
        public required int EmployeeId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
                
        public required string Password { get; set; }
        public required string Position { get; set; }
        public required bool IsActive { get; set; }
    }
}
