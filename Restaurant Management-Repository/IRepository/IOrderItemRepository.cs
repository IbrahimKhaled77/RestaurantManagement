using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.DTOs.OrderDTO;
using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_Repository.IRepository
{
   public interface IOrderItemRepository
    {

        Task<string> AddOrderItem(CreateOrderItemDTO orderItemDTO, [FromHeader] string email, [FromHeader] string password);

        Task<string> DeleteOrderItem(int orderItemId, [FromHeader] string email, [FromHeader] string password);




        Task<List<OrderItemCardDTO>> GetAllOrderItems([FromHeader] string email, [FromHeader] string password);


        Task<OrderItemCardDTO> GetOrderItemById(int id, [FromHeader] string email, [FromHeader] string password);

        Task<string> UpdateOrderItem(UpdateOrderIterm order, [FromHeader] string email, [FromHeader] string password);

 
 
    }
}
