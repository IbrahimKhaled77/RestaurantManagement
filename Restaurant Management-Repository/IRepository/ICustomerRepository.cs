

using RestaurantManagement_Repository.DTOs.CustomerDTO;
using RestaurantManagement_Repository.DTOs.OrderDTO;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.IRepository
{
    public interface ICustomerRepository
    {

        Task<List<CustomerCardDTO>> GetAllCustomers();
        Task<CustomerCardDTO> GetCustomerById(int id);
        Task<string> AddCustomer(CreateCustomerDTO customer);
        Task<string> UpdateCustomer(UpdateCustomerDTO customer);
        Task<string> DeleteCustomer(int id);
    }
}
