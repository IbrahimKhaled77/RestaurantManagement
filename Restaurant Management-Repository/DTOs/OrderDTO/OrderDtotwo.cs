using RestaurantManagement.DTOs.EmployeeOrderDTO;
using RestaurantManagement.DTOs.OrderItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_Repository.DTOs.OrderDTO
{
    public class OrderDtotwo
    {

        public required int OrderId { get; set; }

        public required decimal TotalPrice { get; set; }

        public required bool IsActive { get; set; }

        public required int TableNumber { get; set; }

       

        public virtual List<OrderItemCardDTO>? OrderItems { get; set; }



    }
}
