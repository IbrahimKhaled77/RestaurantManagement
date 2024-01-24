using Microsoft.EntityFrameworkCore;
using Restaurant_Management_Repository.DTOs.AuthanticationDTO;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.IRepository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_Repository.Implementation
{
    public class AuthanticationRepository: IAuthanticationRepository
    {
       

            private readonly RestaurantManagementContext _context;

            public AuthanticationRepository(RestaurantManagementContext context)
            {
                _context = context;
            }

        public async Task<string> Logout(int UserId)
        {
            try
            {
                Log.Information("Starting Logout");
                var Employee = await _context.Employee.FindAsync(UserId);
                var Customer = await _context.Customer.FindAsync(UserId);
                if (Employee != null)
                {
                    Employee.IsLoggedIn = false;
                    Employee.AccessKey = "";
                    Employee.AccesskeyExpireDate = null;
                    _context.Update(Employee);
                    await _context.SaveChangesAsync();
                    Log.Information("LogoutEmployee Is In Finised");
                    Log.Debug($"Debugging LoginEmployee Has been Finised Successfully With finalToken  {Employee.EmployeeId} ");
                    return "LogoutEmployee Is In Finised";
                }
                if (Customer != null)
                {
                    Customer.IsLoggedIn = false;
                    Customer.AccessKey = "";
                    Customer.AccesskeyExpireDate = null;
                    _context.Update(Customer);
                    await _context.SaveChangesAsync();
                    Log.Information("LogoutCustomer Is In Finised");
                    Log.Debug($"Debugging LoginCustomer Has been Finised Successfully With finalToken  {Customer.CustomerId} ");
                    return "LogoutCustomer Is In Finised";
                }

                return "Logout failed try again";

            } catch (Exception ex) {


                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }

        }

        public async Task<string> ResetPassword(ResetPasswordDTO dTO)
        {
            try {

                Log.Information("Starting ResetPassword");
                //check if the Email AND newpassword not equal null
                if (dTO.Email != null && dTO.NewPassword != null)
                {
                    //find the user
                    var Customer = await _context.Customer.Where(x => x.Email == dTO.Email).SingleAsync();
                    var Employee = await _context.Employee.Where(x => x.Email == dTO.Email).SingleAsync();
                    if (Customer != null)
                    {
                        //update the password
                        Customer.Password = dTO.NewPassword;
                        _context.Update(Customer);
                        //savechanges
                        await _context.SaveChangesAsync();
                        Log.Information(" ResetPasswordCustomer Is In Finised");
                        Log.Debug($"Debugging ResetPasswordCustomer Has been Finised Successfully With Customer  {Customer.CustomerId} ");
                        return " Reset Password Customer Is In Finised";
                    }
                    if (Employee != null)
                    {
                        //update the password
                        Employee.Password = dTO.NewPassword;
                        _context.Update(Employee);
                        //savechanges
                        await _context.SaveChangesAsync();
                        Log.Information(" ResetPasswordEmployee Is In Finised");
                        Log.Debug($"Debugging ResetPasswordEmployee  Has been Finised Successfully With Employee  {Employee.EmployeeId} ");
                        return " Reset Password Employee Is In Finised";
                    }

                    throw new Exception("ERROR NOt Found User to ResetPassword ");
                }
                throw new NotImplementedException("Please Fill All Information");
            }
            catch (Exception ex) {

                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

    }
}
