

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs.OrderDTO;

namespace Restaurants_Service.IService
{
    public interface IOrderService
    {

        //Returns are refunded to all Tables
        Task<List<OrderCardDTO>> GetAllOrders([FromHeader] string email, [FromHeader] string password);

        //The Order's return is returned to the one whose ID is equal to the Order's ID
        Task<OrderCardDTO> GetOrderById(int id, [FromHeader] string email, [FromHeader] string password);

        //Create a new Order
        Task<string> AddOrder(CreatOrderDTO order, [FromHeader] string email, [FromHeader] string password);

        //Order Update 
        Task<string> UpdateOrder(UpdateOrderDTO order, [FromHeader] string email, [FromHeader] string password);

        //Order Delete 
        Task<string> DeleteOrder(int id, [FromHeader] string email, [FromHeader] string password);


    }
}
