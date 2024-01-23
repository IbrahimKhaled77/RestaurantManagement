

using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using RestaurantManagement_Repository.DTOs.TableDTO;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.DTOs.OrderDTO
{
    public  class UpdateOrderDTO
    {
        //UpdateTableDto or TableCardDto
        //UpdateOrderIterm or OrderItermCard
        public int OrderId { get; set; }

        public int TableId { get; set; }
        public decimal TotalPrice { get; set; }
        
        public bool IsActive { get; set; }

         public List<UpdateOrderIterm> OrderItems { get; set; }

    }
}
