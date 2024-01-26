using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllMenus();

        Task<Menu> GetMenuById(int menuId);

        Task AddMenus(Menu menu);


        Task UpdateMenu(Menu menu);

        Task DeleteMenu(Menu menu);

    

    }
}
