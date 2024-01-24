

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.DTOs.AuthanticationDTO;
using RestaurantManagement_Repository.DTOs.CustomerDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface ICustomerRepository
    {

        Task<List<CustomerCardDTO>> GetAllCustomers([FromHeader] string email, [FromHeader] string password);
        Task<CustomerCardDTO> GetCustomerById(int id, [FromHeader] string email, [FromHeader] string password);
        Task<string> AddCustomer(CreateCustomerDTO customer);
         Task<string> LoginCustomer(AuthanticationDTOs dto);
        Task<string> UpdateCustomer(UpdateCustomerDTO customer,[FromHeader] string email, [FromHeader] string password);
        Task<string> DeleteCustomer(int id, [FromHeader] string email, [FromHeader] string password);
    }
}
