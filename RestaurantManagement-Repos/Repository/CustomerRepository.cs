
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Context;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Implementation
{
    public class CustomerRepository: ICustomerRepository
    {


        private readonly RestaurantManagemenstContext _context;

        public CustomerRepository(RestaurantManagemenstContext context)
        {
            _context = context;
        }

 

        // Get All of List Customer
        public async Task<List<Customer>> GetAllCustomers()
        {
            return  await _context.Customer.ToListAsync();
        }

        // Get Customer By Customer Id
        public async Task<Customer> GetCustomerById(int CustomerId)
        {
            var customer= await _context.Customer.FirstOrDefaultAsync(x => x.CustomerId == CustomerId);

            return customer!;
        }

        //Add Customer in Database in Table Customer 
        public async Task AddCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
        }

        //Update Customer in Database in Table Customer 
        public async Task UpdateCustomer(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
        }

        //Delete Customer in Database in Table Customer 
        public async Task DeleteCustomerB(Customer customer)
        {
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }

        //Enter the Customer email and password and search for them in the client table in Database
        public async Task<Customer> LoginCustomer(string email, string password)
        {
            var customer= await _context.Customer.SingleOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));

            return customer!;
        }

        //Enter the customer's email and password and search for them in the customer table in the database and
        //see if the customer is active or not and it returns true or false.
        public async Task<bool> IsCustomerLoggedIn( string email, string password)
        {
            var customer= await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true); ;

            return customer!;
        }



    }
}
