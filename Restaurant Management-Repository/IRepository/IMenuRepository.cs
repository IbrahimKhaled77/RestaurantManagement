

using RestaurantManagement_Repository.DTOs.MenuDTO;


namespace RestaurantManagement_Repository.IRepository
{
    public interface IMenuRepository
    {
        Task<List<MenuCardDTO>> GetAllMenus();
        Task<MenuCardDTO> GetMenuById(int id);
        Task<bool> AddMenu(CreatMenuDTO menu);
        Task<bool> UpdateMenu(UpdateMenuDTO menu);
        Task<bool> DeleteMenu(int id);


    }
}
