

using RestaurantManagement.DTOs.EmployeeOrderDTO;
using RestaurantManagement.DTOs.OrderItemDTO;

namespace RestaurantManagement.DTOs.OrderDTO
{
    public class OrderCardDTO
    {

      
        public required int OrderId { get; set; }
                 
        public required decimal TotalPrice { get; set; }
                 
        public required bool IsActive { get; set; }
                 
        public required int TableNumber { get; set; }
                 
        public  virtual List<EmployeeOrderCardDTo>? EmployeeOrder { get; set; }
                 
        public  virtual List<OrderItemCardDTO>? OrderItems { get; set; }


     



    }
}
