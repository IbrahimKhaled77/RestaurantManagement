﻿

using RestaurantManagement_Repository.DTOs.OrderDTO;

namespace RestaurantManagement_Repository.DTOs.CustomerDTO
{
    public class CustomerCardDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        
        public string PhoneNumber {  get; set; }
        public bool IsActive { get; set; }

        public List<OrderCardDTO> Orders { get; set; }
    }
}
