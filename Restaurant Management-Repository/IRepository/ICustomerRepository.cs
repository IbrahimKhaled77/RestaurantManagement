

using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.IRepository
{
    public interface ICustomerRepository
    {

        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<bool> AddCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}
