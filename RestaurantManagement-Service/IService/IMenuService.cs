using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs.MenuDTO;

namespace Restaurants_Service.IService
{
    public interface IMenuService
    {
        //Returns are refunded to all Menus
        Task<List<MenuCardDTO>> GetAllMenus([FromHeader] string email, [FromHeader] string password);

        //The Menu's return is returned to the one whose ID is equal to the Menu's ID
        Task<MenuCardDTO> GetMenuById(int id, [FromHeader] string email, [FromHeader] string password);

        //Create a new Menu
        Task<string> AddMenus(CreatMenuDTO menu, [FromHeader] string email, [FromHeader] string password);

        //Menu Update 
        Task<string> UpdateMenu(UpdateMenuDTO menu, [FromHeader] string email, [FromHeader] string password);

        //Menu Delete 
        Task<string> DeleteMenu(int id, [FromHeader] string email, [FromHeader] string password);


    }
}
