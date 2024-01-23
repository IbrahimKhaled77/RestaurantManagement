

using RestaurantManagement_Repository.DTOs.OrderDTO;
using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.IRepository
{
    public interface IOrderRepository
    {

        Task<List<OrderCardDTO>> GetAllOrders();
        Task<OrderCardDTO> GetOrderById(int id);
        Task<string> AddOrder(CreatOrderDTO order);
        Task<string> UpdateOrder(UpdateOrderDTO order);
        Task<string> DeleteOrder(int id);

    }
}
