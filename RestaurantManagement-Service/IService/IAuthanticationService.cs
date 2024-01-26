
using RestaurantManagement.DTOs.AuthanticationDTO;

namespace Restaurants_Service.IService
{
    public interface IAuthanticationService
    {
        //Logout is shared between the customer and the employee
        Task<string> Logout(int UserId);

        //ResetPassword is shared between the customer and the employee
        Task<string> ResetPassword(ResetPasswordDTO dTO);



    }
}
