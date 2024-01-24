

using RestaurantManagement_Repository.DTOs.EmployeeOrderDTO;
using RestaurantManagement_Repository.DTOs.OrderItemDTO;

namespace RestaurantManagement_Repository.DTOs.OrderDTO
{
    public class OrderCardDTO
    {

      
        public int OrderId { get; set; }
        
        public decimal TotalPrice { get; set; }

        public bool IsActive { get; set; }

        public int TableNumber { get; set; }

        public virtual List<EmployeeOrderCardDTo> EmployeeOrder { get; set; }

        public virtual List<OrderItemCardDTO> OrderItems { get; set; }


     



    }
}
