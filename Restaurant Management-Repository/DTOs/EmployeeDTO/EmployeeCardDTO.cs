﻿



using RestaurantManagement_Repository.DTOs.EmployeeOrderDTO;

namespace RestaurantManagement_Repository.DTOs.EmployeeDTO
{
    public class EmployeeCardDTO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }

        public bool IsActive { get; set; }

        public List<EmployeeOrderCardDTo> EmployeeOrders { get; set; }
    }
}
