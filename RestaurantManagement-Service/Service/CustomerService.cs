using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.AuthanticationDTO;
using RestaurantManagement.DTOs.CustomerDTO;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Repository.Helper;
using Restaurants_Service.IService;
using Serilog;

namespace Restaurants_Service.Service
{
    public class CustomerService: ICustomerService
    {
        private readonly IUnitOfwork _unitOfwork;
        public CustomerService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
           
        }

        public async Task<List<CustomerCardDTO>> GetAllCustomers([FromHeader]  string email, [FromHeader]  string password)
        {
            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);

                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                // Get All of List Customers 
                var Customers = await _unitOfwork.CustomerRepository.GetAllCustomers();
                //Check is Customer equal null
                if (Customers == null)
                {
                    Log.Error($"Customers Not Found ");
                    throw new ArgumentNullException("Customers", "Not Found");
                }

                
                var Customer = from customerDto in Customers
                               select new CustomerCardDTO
                               {
                                   CustomerId = customerDto.CustomerId,
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


        public async Task<CustomerCardDTO> GetCustomerById(int CustomerId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                // Get Customer By CustomerId
                var Customer = await _unitOfwork.CustomerRepository.GetCustomerById(CustomerId);

                // Get Customer Is null
                if (Customer == null)
                {
                    Log.Error($"Customer Not Found ");
                    throw new ArgumentNullException("Customer", "Not Found");
                }

                //Create Customer Card DTO
                CustomerCardDTO customerCardDTO = new CustomerCardDTO() {
                CustomerId = Customer.CustomerId,
                Name = Customer.Name,
                Email = Customer.Email,
                PhoneNumber = Customer.PhoneNumber,
               IsActive = Customer.IsActive,

            };
              
                
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

        public async Task<string> AddCustomer(CreateCustomerDTO customerDTO)
        {
            try
            {
                Log.Information("Create New Customer");

                //Create Customer 
                var customer = new Customer
                {
                    Name = customerDTO.Name,
                    Email = customerDTO.Email,
                    Password = customerDTO.Password,
                    PhoneNumber = customerDTO.PhoneNumber,
                    IsActive = true,
                    IsLoggedIn = false,
                    AccessKey = "",
                    
                };
                // Add Customer 
                await _unitOfwork.CustomerRepository.AddCustomer(customer);

                Log.Information("Customer Is In Finised");
                Log.Debug($"Debugging AddCustomer Has been Finised Successfully With Customer ID  {customer.CustomerId} ");
                return "AddCustomer Has been Finised Successfully ";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Customer Not Found: {ex.Message}");
                throw new DbUpdateException($"Database Error: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in database: {ex.Message}");
                throw new DbUpdateException($"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
            }
        }



        public async Task<string> UpdateCustomer(UpdateCustomerDTO customerDTO, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                //Get Customer By CustomerId
                var Customer = await _unitOfwork.CustomerRepository.GetCustomerById(customerDTO.CustomerId);

                //Check Customer is null
                if (Customer == null)
                {
                    Log.Error($"Customer Not Found ");
                    throw new ArgumentNullException("Customer", "Not Found");
                }
                //Customer Update
                Customer.Name = customerDTO.Name;
                Customer.Email = customerDTO.Email;
                Customer.Password = customerDTO.Password;
                Customer.PhoneNumber = customerDTO.PhoneNumber;
                Customer.IsActive = customerDTO.IsActive;

                //Customer Update in Database 
                await _unitOfwork.CustomerRepository.UpdateCustomer(Customer);

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


        public async Task<string> DeleteCustomer(int CustomerId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in And must be Position (Admin) 
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                //Get Customer By CustomerId
                var Customer = await _unitOfwork.CustomerRepository.GetCustomerById(CustomerId);

                //Check Customer is null
                if (Customer == null)
                {

                    Log.Error($"Customer Not Found ");
                    throw new ArgumentNullException("Customer", "Not Found");
                }
                Log.Information("Customer Is In Existing");

                //Customer Delete
                await _unitOfwork.CustomerRepository.DeleteCustomerB(Customer);
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

        public async Task<string> LoginCustomer(AuthanticationDTOs AuthanticationDTOs)
        {
            try
            {
                Log.Information("Starting Login");
                //check if the Email equal null Or Empty

                if (string.IsNullOrEmpty(AuthanticationDTOs.Email))
                    throw new Exception("Email Is Required");

                //check if the Password equal null Or Empty
                if (string.IsNullOrEmpty(AuthanticationDTOs.Password))
                    throw new Exception("Password Is Required");

                // Get Customer 
                var customer = await _unitOfwork.CustomerRepository.LoginCustomer(AuthanticationDTOs.Email, AuthanticationDTOs.Password);

                //check if the customer not equal null
                if (customer != null)
                {

                    if (!customer.IsLoggedIn)
                    {

                        //Create a token Customer
                        Helper.TokenCustomer(customer);
                        //Customer Update 
                        await _unitOfwork.CustomerRepository.UpdateCustomer(customer);

                        Log.Information("LoginCustomer Is In Finised");
                        Log.Debug($"Debugging LoginCustomer Has been Finised Successfully With finalToken  {Helper.TokenCustomer(customer)} ");
                        return Helper.TokenCustomer(customer); ;
                    }

                    else
                    {
                        //Logout 
                        customer.IsLoggedIn = false;
                        //Customer Update
                        await _unitOfwork.CustomerRepository.UpdateCustomer(customer);

                        Log.Information("Customer Is not Found");
                        Log.Debug($"Youre Session Has been Closed Please Login in Again");

                        return "Youre Session Has been Closed Please Login in Again";
                    }
                }
                else
                {
                    Log.Information("Either Email or Password is Incorrect");
                    Log.Debug($"Either Email or Password is Incorrect");
                    return "Either Email or Password is Incorrect";
                }


            }
            catch (Exception ex)
            {

                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
            }
        }



    }
}
