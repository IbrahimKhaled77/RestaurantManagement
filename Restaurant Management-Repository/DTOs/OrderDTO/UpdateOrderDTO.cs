

using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using RestaurantManagement_Repository.DTOs.TableDTO;

namespace RestaurantManagement_Repository.DTOs.OrderDTO
{
    public  class UpdateOrderDTO
    {
        //UpdateTableDto or TableCardDto
        //UpdateOrderIterm or OrderItermCard
        public int OrderId { get; set; }
        public bool IsActive { get; set; }
        
        public decimal TotalPrice { get; set; }
        public UpdateTableDto TableNumber { get; set; }

        public List<UpdateOrderIterm> OrderItems { get; set; }

    }
}
