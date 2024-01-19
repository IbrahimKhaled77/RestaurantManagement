

using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly RestaurantManagementContext _context;

        public OrderRepository(RestaurantManagementContext context)
        {
            _context = context;
        }
        public Task<bool> AddOrder(Order order)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> DeleteOrder(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<IEnumerable<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
    }
}
