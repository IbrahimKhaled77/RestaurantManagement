

using RestaurantManagement_Repository.DTOs.EmployeeDTO;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.IRepository
{
    public interface IEmployeeRepository
    {

        Task<List<EmployeeCardDTO>> GetAllEmployees();
        Task<EmployeeCardDTO> GetEmployeeById(int id);
        Task<string> AddEmployee(CreatEmployeeDTO employee);
        Task<string> UpdateEmployee(UpdateEmployeeDTO employee);
        Task<string> DeleteEmployee(int id);


    }
}
