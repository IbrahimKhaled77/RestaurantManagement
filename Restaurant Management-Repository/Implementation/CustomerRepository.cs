

using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.CustomerDTO;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;

namespace RestaurantManagement_Repository.Implementation
{
    public class CustomerRepository: ICustomerRepository
    {


        private readonly RestaurantManagementContext _context;

        public CustomerRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public  async Task<string> AddCustomer(CreateCustomerDTO customerDTO)
        {
           
            try
            {
                Log.Information("Create New Customer");
                var Customer=new Customer();   
                Customer.Name=customerDTO.Name;
                Customer.Email = customerDTO.Email;
                Customer.Password = customerDTO.Password;
                Customer.PhoneNumber = customerDTO.PhoneNumber;

                _context.Customer.Add(Customer);
                await _context.SaveChangesAsync();
                Log.Information("Customer Is In Finised");
                Log.Debug($"Debugging AddCustomer Has been Finised Successfully With Customer ID  {Customer.CustomerId} ");
                return " AddCustomer Has been Finised Successfully ";

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Customer Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");
                
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
               
            }
        }

        public async Task<string> DeleteCustomer(int id)
        {
            
          

            try
            {
                var Customer = await _context.Customer.FindAsync(id);
                if(Customer == null) {

                    Log.Error($"Customer Not Found ");
                    throw new ArgumentNullException("Customer", "Not Found");
                }
                Log.Information("Customer Is In Existing");

                _context.Customer.Remove(Customer);
                await _context.SaveChangesAsync();
                Log.Information("Customer Is Deleted");
                Log.Debug($"Debugging DeleteCustomer Has been Finised Successfully With Customer ID  = {Customer.CustomerId}");
                return "DeleteCustomer Has been Finised Successfully ";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Customer Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");
            
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
                
            }
        }

        public async Task<List<CustomerCardDTO>> GetAllCustomers()
        {
            
            try
            {
                var Customers = await _context.Customer.ToListAsync();
                if (Customers == null)
                {
                    Log.Error($"Customers Not Found ");
                    throw new ArgumentNullException("Customers", "Not Found");
                }

                var Customer = from customerDto in Customers
                             select new CustomerCardDTO
                             {
                                CustomerId= customerDto.CustomerId,
                                 Name = customerDto.Name,
                                 Email = customerDto.Email,
                                 PhoneNumber = customerDto.PhoneNumber,
                                 IsActive = customerDto.IsActive,
                                 



                             };
                Log.Information("Customers are Reached");
                Log.Debug($"Debugging GetAllCustomers Has been Finised Successfully");
                return (Customer.ToList());




            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Customer Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public async Task<CustomerCardDTO> GetCustomerById(int id)
        {
           
            try
            {
                var Customer = await _context.Customer.FirstOrDefaultAsync(value=>value.CustomerId==id);
                if (Customer == null)
                {
                    Log.Error($"Customer Not Found ");
                    throw new ArgumentNullException("Customer", "Not Found");
                }
                var customerCardDTO = new CustomerCardDTO();
                customerCardDTO.CustomerId = Customer.CustomerId;
                customerCardDTO.Name = Customer.Name;
                customerCardDTO.Email = Customer.Email;
                customerCardDTO.PhoneNumber = Customer.PhoneNumber;
                customerCardDTO.IsActive = Customer.IsActive;
                Log.Information("Customer Is Reached");
                Log.Debug($"Debugging GetCustomerById Has been Finised Successfully With Customer ID  = {Customer.CustomerId}");
                return (customerCardDTO);

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Customer Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public async  Task<string> UpdateCustomer(UpdateCustomerDTO customerDTO)
        {
            
            try
            {
                var Customer = await _context.Customer.FindAsync(customerDTO.CustomerId);
                if (Customer == null)
                {
                    Log.Error($"Customer Not Found ");
                    throw new ArgumentNullException("Customer", "Not Found");
                }
                Customer.Name = customerDTO.Name;
                Customer.Email = customerDTO.Email;
                Customer.Password = customerDTO.Password; 
                Customer.PhoneNumber = customerDTO.PhoneNumber;
                Customer.IsActive = true;

               _context.Customer.Update(Customer);
                await _context.SaveChangesAsync();
                Log.Information("Customer Is Updated");
                Log.Debug($"Debugging UpdateCustomer Has been Finised Successfully With Customer ID  = {Customer.CustomerId}");
                return "UpdateCustomer Has been Finised Successfully";

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Customer Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }
    }
}
