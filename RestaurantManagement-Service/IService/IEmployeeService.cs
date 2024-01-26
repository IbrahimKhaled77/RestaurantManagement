using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs.AuthanticationDTO;
using RestaurantManagement.DTOs.EmployeeDTO;

namespace Restaurants_Service.IService
{
    public interface IEmployeeService
    {

        //Returns are refunded to all Employees
        Task<List<EmployeeCardDTO>> GetAllEmployees([FromHeader] string Email, [FromHeader] string Password);

        //The Employee's return is returned to the one whose ID is equal to the Employee's ID
        Task<EmployeeCardDTO> GetEmployeeById(int EmployeeId, [FromHeader] string Email, [FromHeader] string Password);

        //Create a new Employee
        Task<string> AddEmployee(CreatEmployeeDTO Employee,[FromHeader] string Email, [FromHeader] string Passwor);

        //Employee Update 
        Task<string> UpdateEmployee(UpdateEmployeeDTO Employee, [FromHeader] string Email, [FromHeader] string Password);

        //Employee Delete 
        Task<string> DeleteEmployee(int EmployeeId, [FromHeader] string Email, [FromHeader] string Password);

        //Employee login
        Task<string> LoginEmployee(AuthanticationDTOs AuthanticationDTOs);







    }
}
