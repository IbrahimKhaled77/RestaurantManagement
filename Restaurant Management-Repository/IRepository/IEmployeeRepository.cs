

using RestaurantManagement_Repository.DTOs.EmployeeDTO;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.IRepository
{
    public interface IEmployeeRepository
    {

        Task<List<EmployeeCardDTO>> GetAllEmployees();
        Task<EmployeeCardDTO> GetEmployeeById(int id);
        Task<bool> AddEmployee(CreatEmployeeDTO employee);
        Task<bool> UpdateEmployee(UpdateEmployeeDTO employee);
        Task<bool> DeleteEmployee(int id);


    }
}
