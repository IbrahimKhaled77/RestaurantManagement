


using Microsoft.EntityFrameworkCore;
using Restaurant_Management_Repository.DTOs.AuthanticationDTO;
using RestaurantManagement_Repository.DTOs.AuthanticationDTO;

namespace RestaurantManagement_Repository.IRepository
{
    public interface IAuthanticationRepository
    {


        // Task<bool> Login(AuthanticationDTOs dto);


          Task<string> Logout(int UserId);
        Task<string> ResetPassword(ResetPasswordDTO dTO);
    
    }
}
