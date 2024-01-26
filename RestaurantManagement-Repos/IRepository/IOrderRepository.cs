using RestaurantManagement.DTOs.OrderDTO;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface IOrderRepository
    {
        Task<List<OrderCardDTO>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task AddOrder(Order Menu);
       
        Task UpdateOrder(Order Menu);

        Task DeleteOrder(Order Menu);

  
        Task SaveChangesAsync();

    }
}
