using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface IEmployeeRepository

    {
        Task<List<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeById(int employeeId);

        Task AddEmployee(Employee employee);

        Task UpdateEmployee(Employee employee);
      
        Task DeleteEmployee(Employee employee);

        Task<Employee> LoginEmployee(string email, string password);

        Task<bool> IsEmployeeLoggedIn(string email, string password, string position);

        Task<bool> IsEmployeeLoggedIn(string email, string password);

        Task<bool> IsEmployeeLoggedAdminIn(string email, string password, string position);

    }
}
