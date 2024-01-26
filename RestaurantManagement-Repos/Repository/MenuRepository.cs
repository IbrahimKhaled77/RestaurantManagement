
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Context;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Implementation
{
    public class MenuRepository: IMenuRepository
    {
        private readonly RestaurantManagemenstContext _context;

        public MenuRepository(RestaurantManagemenstContext context)
        {
            _context = context;
        }

        // Get All of List Customer
        public async Task<List<Menu>> GetAllMenus()
        {

            return await _context.Menu.ToListAsync();
        }


        // Get Menu By Menu Id
        public async Task<Menu> GetMenuById(int menuId)
        {
            var menu= await _context.Menu.FirstOrDefaultAsync(x => x.MenuId == menuId);

            return menu!;

        }

        //Add Menu in Database in Table Menu 
        public async Task AddMenus(Menu menu)
        {
            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

        }

        //Update Menu in Database in Table Menu 
        public async Task UpdateMenu(Menu Menu)
        {
            _context.Menu.Update(Menu);
            await _context.SaveChangesAsync();
        }

        //Delete Menu in Database in Table Menu 
        public async Task DeleteMenu(Menu Menu)
        {

            _context.Menu.Remove(Menu);
            await _context.SaveChangesAsync();
        }

       

    }
}
