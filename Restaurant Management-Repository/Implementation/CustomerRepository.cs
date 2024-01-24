

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.AuthanticationDTO;
using RestaurantManagement_Repository.DTOs.CustomerDTO;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                Customer.PhoneNumber = customerDTO.PhoneNumber;
                Customer.IsActive = true;
                Customer.IsLoggedIn = false;
                Customer.AccessKey = "";
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

        public async Task<string> LoginCustomer(AuthanticationDTOs AuthanticationDTOs)
        {
            try
            {
                Log.Information("Starting Login");
                if (string.IsNullOrEmpty(AuthanticationDTOs.Email))
                    throw new Exception("Email Is Required");
                if (string.IsNullOrEmpty(AuthanticationDTOs.Password))
                    throw new Exception("Password Is Required");
                var customer = _context.Customer.SingleOrDefault(x =>x.Email.Equals(AuthanticationDTOs.Email) && x.Password.Equals(AuthanticationDTOs.Password));
                if (customer != null)
                {
                    if (!customer.IsLoggedIn)
                    {
                      
                        //2-JWT 
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL");
                        var tokenDescriptior = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                        new Claim("UserId",customer.CustomerId.ToString()),
                        new Claim("Name",customer.Name)
                            }),
                            Expires = DateTime.Now.AddHours(3),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey) , SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptior);//encoding
                        string finalToken = tokenHandler.WriteToken(token);
                        customer.IsLoggedIn = true;
                        customer.AccessKey = finalToken;
                        customer.AccesskeyExpireDate= tokenDescriptior.Expires;
                        _context.Customer.Update(customer);
                        await _context.SaveChangesAsync();
                        Log.Information("LoginCustomer Is In Finised");
                        Log.Debug($"Debugging LoginCustomer Has been Finised Successfully With finalToken  {finalToken} ");


                        return finalToken;
                    }

                    else
                    {
                        customer.IsLoggedIn = false;
                        _context.Update(customer);
                        await _context.SaveChangesAsync();

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
            catch (Exception ex) {

                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }




        }

        public async Task<string> DeleteCustomer(int id, [FromHeader] string email, [FromHeader] string password)
        {
            
          

            try
            {

                var isAdminLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password  && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

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

        public async Task<List<CustomerCardDTO>> GetAllCustomers([FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {

                var isAdminLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password  && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

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

        public async Task<CustomerCardDTO> GetCustomerById(int id, [FromHeader] string email, [FromHeader] string password)
        {
           
            try
            {

                var isAdminLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

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

        public async  Task<string> UpdateCustomer(UpdateCustomerDTO customerDTO, [FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {
                
                var isAdminLoggedInCustomer = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if ( !isAdminLoggedInCustomer)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

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
