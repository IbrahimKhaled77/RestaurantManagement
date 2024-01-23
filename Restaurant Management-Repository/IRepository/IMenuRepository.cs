

using RestaurantManagement_Repository.DTOs.MenuDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface IMenuRepository
    {
        Task<List<MenuCardDTO>> GetAllMenus();
        Task<MenuCardDTO> GetMenuById(int id);
        Task<string> AddMenus(CreatMenuDTO menu);
        Task<string> UpdateMenu(UpdateMenuDTO menu);
        Task<string> DeleteMenu(int id);


    }
}
