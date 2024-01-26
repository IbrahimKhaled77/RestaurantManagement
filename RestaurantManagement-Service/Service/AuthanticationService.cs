using RestaurantManagement.DTOs.AuthanticationDTO;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Service.IService;
using Serilog;

namespace Restaurants_Service.Service
{
    public class AuthanticationService : IAuthanticationService
    {

      
        private readonly IUnitOfwork _unitOfwork;

        public AuthanticationService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
             
        }

        public async Task<string> Logout(int UserId)
        {
            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.AuthanticationRepository.IsLogoutCustomer(UserId);

                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.AuthanticationRepository.IsLogoutEmployee(UserId);

                //check if the Email AND newpassword not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                
                Log.Information("Starting Logout");

                // Get Employee By Employee Id
                var Employee = await _unitOfwork.EmployeeRepository.GetEmployeeById(UserId);

                // Get Customer By Customer Id
                var Customer = await _unitOfwork.CustomerRepository.GetCustomerById(UserId);

                if (Employee != null)
                {
                    //Accesskey is Token
                   
                    Employee.IsLoggedIn = false;
                    Employee.AccessKey = "";
                    Employee.AccesskeyExpireDate = null;

                    //Update Employee in Database in Table Employee 
                    await _unitOfwork.EmployeeRepository.UpdateEmployee(Employee);

                    Log.Information("LogoutEmployee Is In Finised");
                    Log.Debug($"Debugging LoginEmployee Has been Finised Successfully With finalToken  {Employee.EmployeeId} ");
                    return "LogoutEmployee Is In Finised";
                }
                else if (Customer != null)
                {
                    //Accesskey is Token
                    Customer.IsLoggedIn = false;
                    Customer.AccessKey = "";
                    Customer.AccesskeyExpireDate = null;

                    //Update Customer in Database in Table Customer 
                    await _unitOfwork.CustomerRepository.UpdateCustomer(Customer);

                     Log.Information("LogoutCustomer Is In Finised");
                    Log.Debug($"Debugging LoginCustomer Has been Finised Successfully With finalToken  {Customer.CustomerId} ");
                    return "LogoutCustomer Is In Finised";
                }

                return "Logout failed try again";

            }
            catch (Exception ex)
            {


                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }

        }

        public async Task<string> ResetPassword(ResetPasswordDTO dTO)
        {
            try
            {

                Log.Information("Starting ResetPassword");
                //check if the Email AND newpassword not equal null
                if (dTO.Email != null && dTO.NewPassword != null)
                {
                    //find the Customer
                    var Customer = await _unitOfwork.AuthanticationRepository.IsResetPasswordCustomer(dTO.Email);
                    //find the Employee
                    var Employee = await _unitOfwork.AuthanticationRepository.IsResetPasswordEmployee(dTO.Email);
                    //check if not equal null
                    if (Customer != null)
                    {


                        //update the password
                        Customer.Password = dTO.NewPassword;
                        //update the Customer And savechanges
                        await _unitOfwork.CustomerRepository.UpdateCustomer(Customer);
                      
                      
                        Log.Information(" ResetPasswordCustomer Is In Finised");
                        Log.Debug($"Debugging ResetPasswordCustomer Has been Finised Successfully With Customer  {Customer.CustomerId} ");
                        return " Reset Password Customer Is In Finised";
                    }
                    else if (Employee != null)
                    {

                        //update the password
                        Employee.Password = dTO.NewPassword;
                        //update the Customer And savechanges
                        await _unitOfwork.EmployeeRepository.UpdateEmployee(Employee);
                        
                        
                        Log.Information(" ResetPasswordEmployee Is In Finised");
                        Log.Debug($"Debugging ResetPasswordEmployee  Has been Finised Successfully With Employee  {Employee.EmployeeId} ");
                        return " Reset Password Employee Is In Finised";
                    }
                    else
                    {
                        throw new Exception("ERROR NOt Found User to ResetPassword ");
                    }


                }
                else
                {
                    throw new NotImplementedException("Please Fill All Information");

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
