using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface IAuthanticationRepository
    {
        Task<bool> IsLogoutCustomer(int customerId);

        Task<bool> IsLogoutEmployee(int employeeId);

        Task<Customer> IsResetPasswordCustomer(string emailCustomer);

        Task<Employee> IsResetPasswordEmployee(string emailEmployeeId);

       
    }
}
