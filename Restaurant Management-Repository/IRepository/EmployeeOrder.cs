

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.DTOs.EmployeeOrderCardDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface IEmployeeOrderRepository
    
    {

      //Admin
        Task<string> AddEmployeeOrder(EmployeeOrderCreatDTOs order, [FromHeader] string email, [FromHeader] string password);

    }
}
