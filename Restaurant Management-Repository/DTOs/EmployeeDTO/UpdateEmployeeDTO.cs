

namespace RestaurantManagement_Repository.DTOs.EmployeeDTO
{
    public class UpdateEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
    }
}
