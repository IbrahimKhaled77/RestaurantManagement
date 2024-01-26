using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface ICustomerRepository
    {

        Task<List<Customer>> GetAllCustomers();

        Task<Customer> GetCustomerById(int CustomerId);

        Task AddCustomer(Customer customer);

        Task UpdateCustomer(Customer customer);

        Task DeleteCustomerB(Customer customer); 

        Task<bool> IsCustomerLoggedIn(string email, string password);

        Task<Customer> LoginCustomer(string email, string password);
      
     
     
    
     

     
       
    }
}
