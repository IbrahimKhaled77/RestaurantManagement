using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs.AuthanticationDTO;
using RestaurantManagement.DTOs.CustomerDTO;

namespace Restaurants_Service.IService
{
    public interface ICustomerService
    {
        //Returns are refunded to all customers
        Task<List<CustomerCardDTO>> GetAllCustomers([FromHeader] string email, [FromHeader] string password);

        //The customer's return is returned to the one whose ID is equal to the customer's ID
        Task<CustomerCardDTO> GetCustomerById(int id, [FromHeader] string email, [FromHeader] string password);

        //Create a new Customer
        Task<string> AddCustomer(CreateCustomerDTO customer);

        //Customer Update 
        Task<string> UpdateCustomer(UpdateCustomerDTO customer, [FromHeader] string email, [FromHeader] string password);

        //Customer Delete 
        Task<string> DeleteCustomer(int id, [FromHeader] string email, [FromHeader] string password);

        //Customer login
        Task<string> LoginCustomer(AuthanticationDTOs dto);

    }
}
