

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantManagement_Repository.Context;

using RestaurantManagement_Repository.DTOs.MenuDTO;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;

namespace RestaurantManagement_Repository.Implementation
{
    public class MenuRepository: IMenuRepository
    {
        private readonly RestaurantManagementContext _context;

        public MenuRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public  async Task<string> AddMenus(CreatMenuDTO menu)
        {
            
            try
            {
                Log.Information("Menu Is In Procesing");
                Menu Menu1 = new Menu();
                Menu1.Description = menu.Description;
                Menu1.Name = menu.Name;
                Menu1.Price = menu.Price;
                Menu1.IsActive = true;

                _context.Menu.Add(Menu1);
                await _context.SaveChangesAsync();
                Log.Information("Menu Is In Finised");
                Log.Debug($"Debugging AddMenus Has been Finised Successfully With Menu ID  {Menu1.MenuId} ");
                return "AddMenus Has been Finised Successfully";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Menu Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");
              
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
             
            }
        }

        public  async Task<string> DeleteMenu(int id)
        {
            
            try
            {
                var menu = await _context.Menu.FindAsync(id);
                if (menu == null)
                {
                    Log.Error($"Menu Not Found ");
                    throw new ArgumentNullException("Menu", "Not Found Menu ");

                }
                Log.Information("Menu Is In Existing");
                _context.Menu.Remove(menu);
                await _context.SaveChangesAsync();
                Log.Information("Menu Is Deleted");
                Log.Debug($"Debugging DeleteMenu Has been Finised Successfully With Table ID  = {menu.MenuId}");
                return "DeleteMenu Has been Finised Successfully";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Menu Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public async Task<List<MenuCardDTO>> GetAllMenus()
        {
            
            try
            {
                var menus = await _context.Menu.ToListAsync();
                if (menus == null)
                {
                    Log.Error($"Menus Not Found ");
                    throw new ArgumentNullException("menus", "Not Found menus");

                }
              
                var menu = from menuCardDTO in menus
                           select new MenuCardDTO
                               {
                                 
                               MenuId = menuCardDTO.MenuId,
                               Name = menuCardDTO.Name,
                               Description = menuCardDTO.Description,  
                               Price=menuCardDTO.Price,
                               IsActive = menuCardDTO.IsActive,



                               };
                Log.Information("menus are Reached");
                Log.Debug($"Debugging GetAllMenus Has been Finised Successfully");
                return (menu.ToList());



            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Menu Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception : {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public  async Task<MenuCardDTO> GetMenuById(int id)
        {
            
            try
            {
                var menu = await _context.Menu.FirstOrDefaultAsync(value => value.MenuId == id);
                if (menu == null)
                {

                    Log.Error($"menu Not Found ");
                    throw new ArgumentNullException("menu", "Not Found menu");
                }
                   

                var MenuCardDTO = new MenuCardDTO();
                MenuCardDTO.MenuId = menu.MenuId;
                MenuCardDTO.Name = menu.Name;  
                MenuCardDTO.Description = menu.Description;
                MenuCardDTO.Price = menu.Price;
                MenuCardDTO.IsActive = menu.IsActive;
                Log.Information("menu Is Reached");
                Log.Debug($"Debugging GetMenuById Has been Finised Successfully With Menu ID  = {menu.MenuId}");
                return (MenuCardDTO);
            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Menu Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception : {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public async Task<string> UpdateMenu(UpdateMenuDTO MenuDto)
        {
            try
            {
                var menu = await _context.Menu.FindAsync(MenuDto.MenuId);
                if (menu == null)
                {
                    Log.Error($"Menu Not Found ");
                    throw new ArgumentNullException("Menu", "Not Found Menu");
                }
                Log.Information("Menu Is  Existing");
                menu.Name = MenuDto.Name;
                menu.Description = MenuDto.Description;
                menu.Price = MenuDto.Price;
                menu.IsActive = true;

                _context.Menu.Update(menu);
                await _context.SaveChangesAsync();
                Log.Information("Menu Is Updated");
                Log.Debug($"Debugging UpdateMenu Has been Finised Successfully With Menu ID  = {menu.MenuId}");
                return "UpdateMenu Has been Finised Successfully ";

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Menu Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }
    }
}
