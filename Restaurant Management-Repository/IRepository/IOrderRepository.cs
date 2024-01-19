

using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.IRepository
{
    public interface IOrderRepository
    {

        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<bool> AddOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);

    }
}
