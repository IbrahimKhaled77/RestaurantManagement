

namespace RestaurantManagement.DTOs.CustomerDTO
{
    public class UpdateCustomerDTO
    {
        public int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }

        public required bool IsActive { get;set;}
       
    }
}
