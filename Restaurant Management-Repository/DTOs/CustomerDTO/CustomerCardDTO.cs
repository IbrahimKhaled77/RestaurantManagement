


namespace RestaurantManagement.DTOs.CustomerDTO
{
    public class CustomerCardDTO
    {
        public required int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }


        public required string PhoneNumber {  get; set; }
        public required bool IsActive { get; set; }

   
    }
}
