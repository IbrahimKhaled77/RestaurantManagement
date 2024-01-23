

using RestaurantManagement_Repository.DTOs.EmployeeOrderCardDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface IEmployeeOrderRepository
    
    {

      
        Task<string> AddEmployeeOrder(EmployeeOrderCreatDTOs order);

    }
}
