﻿

namespace RestaurantManagement_Repository.DTOs.CustomerDTO
{
    public class UpdateCustomerDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
