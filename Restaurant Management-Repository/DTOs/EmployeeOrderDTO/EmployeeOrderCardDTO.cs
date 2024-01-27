

namespace RestaurantManagement.DTOs.EmployeeOrderDTO
{
    public class EmployeeOrderCardDTo
    {


        public required  int EmployeeOrderId { get; set; }

        public required int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }
        public required int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    
        public int tableNumber { get; set; }



        

       



    }
}
