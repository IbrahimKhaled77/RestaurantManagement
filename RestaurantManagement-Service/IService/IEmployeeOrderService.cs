
using RestaurantManagement.DTOs.EmployeeOrderCardDTO;

namespace Restaurants_Service.IService
{
    public interface IEmployeeOrderService
    {
        //Add the employees who will execute the order
        Task<string> AddEmployeeOrder(EmployeeOrderCreatDTOs employeeOrderDto, string email, string password);

    }
}
