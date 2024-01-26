
namespace RestaurantManagement.DTOs.EmployeeDTO
{
    public class CreatEmployeeDTO
    {
       
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }
        public required string Position { get; set; }
       
    }

}
