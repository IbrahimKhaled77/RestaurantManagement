using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Context;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Implementation
{
    public class AuthanticationRepository: IAuthanticationRepository
    {
       private readonly RestaurantManagemenstContext _context;

       public AuthanticationRepository(RestaurantManagemenstContext context)
        {
          _context = context;
        }

        //Searches for the customerId in the Customer table. The customer must be logged in
        public async Task<bool> IsLogoutCustomer(int customerId) {

           return await _context.Customer.AnyAsync(x => x.CustomerId == customerId && x.IsLoggedIn == true);
        }

        //Searches for the employeeId in the Employee table. The employee must be logged in
        public async Task<bool> IsLogoutEmployee(int employeeId)
        {

            return await _context.Employee.AnyAsync(x => x.EmployeeId == employeeId && x.IsLoggedIn == true);
        }

        //It searches for the email of the employee entered, is it the same as the email in the employee table,
        //and returns the employee to the one that is equal
        public async Task<Employee> IsResetPasswordEmployee(string EmailEmployee)
        {
            var employee = await _context.Employee.Where(x => x.Email == EmailEmployee).FirstOrDefaultAsync();

            return employee!;
        }

        //It searches for the email of the Customer entered, is it the same as the email in the Customer table,
        //and returns the Customer to the one that is equal
        public async Task<Customer> IsResetPasswordCustomer(string EmailCustomer)
        {
            var customer = await _context.Customer.Where(x => x.Email == EmailCustomer).FirstOrDefaultAsync();

            return customer!;
        }


       

    }
}
