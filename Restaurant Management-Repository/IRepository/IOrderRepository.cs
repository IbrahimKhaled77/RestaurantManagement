

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.DTOs.OrderDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface IOrderRepository
    {

       
        Task<List<OrderCardDTO>> GetAllOrders([FromHeader] string email, [FromHeader] string password);

        
        Task<OrderCardDTO> GetOrderById(int id, [FromHeader] string email, [FromHeader] string password);
        
        //Admin
        Task<string> AddOrder(CreatOrderDTO order, [FromHeader] string email, [FromHeader] string password);

        //Admin
        Task<string> UpdateOrder(UpdateOrderDTO order, [FromHeader] string email, [FromHeader] string password);

        //Admin
        Task<string> DeleteOrder(int id, [FromHeader] string email, [FromHeader] string password);

    }
}
