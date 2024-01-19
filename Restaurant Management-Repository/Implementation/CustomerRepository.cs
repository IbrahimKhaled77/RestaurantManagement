

using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.Implementation
{
    public class CustomerRepository: ICustomerRepository
    {


        private readonly RestaurantManagementContext _context;

        public CustomerRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public Task<bool> AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> DeleteCustomer(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<IEnumerable<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<Customer> GetCustomerById(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> UpdateCustomer(Customer customer)
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
