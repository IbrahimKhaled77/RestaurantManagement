

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.DTOs.MenuDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface IMenuRepository
    {
      
        Task<List<MenuCardDTO>> GetAllMenus([FromHeader] string email, [FromHeader] string password);
        
        Task<MenuCardDTO> GetMenuById(int id, [FromHeader] string email, [FromHeader] string password);
        //Admin
        Task<string> AddMenus(CreatMenuDTO menu, [FromHeader] string email, [FromHeader] string password);
        //Admin
        Task<string> UpdateMenu(UpdateMenuDTO menu, [FromHeader] string email, [FromHeader] string password);
        //Admin
        Task<string> DeleteMenu(int id, [FromHeader] string email, [FromHeader] string password);


    }
}
