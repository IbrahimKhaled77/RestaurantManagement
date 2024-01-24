

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.DTOs.AuthanticationDTO;
using RestaurantManagement_Repository.DTOs.EmployeeDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface IEmployeeRepository
    {

        Task<List<EmployeeCardDTO>> GetAllEmployees([FromHeader] string email, [FromHeader] string password);
        Task<EmployeeCardDTO> GetEmployeeById(int id, [FromHeader] string email, [FromHeader] string password);
        Task<string> AddEmployee(CreatEmployeeDTO employee);

       
        Task<string> UpdateEmployee(UpdateEmployeeDTO employee, [FromHeader] string email, [FromHeader] string password);
        Task<string> LoginEmployee(AuthanticationDTOs dto);

        
        Task<string> DeleteEmployee(int id,[FromHeader] string email, [FromHeader] string password);


    }
}
