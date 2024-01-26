



namespace RestaurantManagement.DTOs.OrderDTO
{
    public  class UpdateOrderDTO
    {
       
        public required  int OrderId { get; set; }
                
        public required  int TableId { get; set; }
        public required  decimal TotalPrice { get; set; }
                
        public required  bool IsActive { get; set; }

       

    }
}
