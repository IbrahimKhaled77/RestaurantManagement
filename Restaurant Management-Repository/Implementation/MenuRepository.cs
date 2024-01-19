

using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.MenuDTO;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement_Repository.Implementation
{
    public class MenuRepository: IMenuRepository
    {
        private readonly RestaurantManagementContext _context;

        public MenuRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public Task<bool> AddMenu(CreatMenuDTO menu)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> DeleteMenu(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<List<MenuCardDTO>> GetAllMenus()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<MenuCardDTO> GetMenuById(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> UpdateMenu(UpdateMenuDTO menu)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
    }
}
