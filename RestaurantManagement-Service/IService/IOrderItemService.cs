
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs.OrderItemDTO;

namespace Restaurants_Service.IService
{
    public interface IOrderItemService
    {

        //Returns are refunded to all OrderItems
        Task<List<OrderItemCardDTO>> GetAllOrderItems([FromHeader] string email, [FromHeader] string password);

        //The OrderItem's return is returned to the one whose ID is equal to the OrderItem's ID
        Task<OrderItemCardDTO> GetOrderItemById(int id, [FromHeader] string email, [FromHeader] string password);

        //Create a new OrderItem
        Task<string> AddOrderItem(CreateOrderItemDTO orderItemDTO, [FromHeader] string email, [FromHeader] string password);

        //OrderItem Update 
        Task<string> UpdateOrderItem(UpdateOrderIterm order, [FromHeader] string email, [FromHeader] string password);

        //OrderItem Delete 
        Task<string> DeleteOrderItem(int orderItemId, [FromHeader] string email, [FromHeader] string password);

  




    }
}
