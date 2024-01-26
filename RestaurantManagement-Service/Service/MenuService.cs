using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.MenuDTO;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Service.IService;
using Serilog;

namespace Restaurants_Service.Service
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfwork _unitOfwork;
        public MenuService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
           ;
        }
    
        public async Task<List<MenuCardDTO>> GetAllMenus([FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork. CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedIn(email,password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                // Get All of List menus 
                var menus = await _unitOfwork. MenuRepository.GetAllMenus();
                //Check is menu equal null
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
                               Price = menuCardDTO.Price,
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
        public async Task<MenuCardDTO> GetMenuById(int MenuId, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Get Menu By MenuId
                var menu = await _unitOfwork.MenuRepository.GetMenuById(MenuId);
                //Check Menu is null
                if (menu == null)
                {

                    Log.Error($"menu Not Found ");
                    throw new ArgumentNullException("menu", "Not Found menu");
                }

                //Create Menu Card DTO
                var MenuCardDTO = new MenuCardDTO()
                {
                     MenuId = menu.MenuId,
                     Name = menu.Name,
                     Description = menu.Description,
                     Price = menu.Price,
                     IsActive = menu.IsActive,

                 };

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

        public async Task<string> AddMenus(CreatMenuDTO menu, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");

                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");
                //Check Is Admin 
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }


                Log.Information("Menu Is In Procesing");

                //Create New Menu 
                Menu Menu1 = new Menu()
                {
                    Description = menu.Description,
                    Name = menu.Name,
                    Price = menu.Price,
                    IsActive = true,
                };
                // Add Menu And SaveChanges IN Database 
                await _unitOfwork.MenuRepository.AddMenus(Menu1);

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


        public async Task<string> UpdateMenu(UpdateMenuDTO MenuDto, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn =  await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");

                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork.  EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");
                //Check Is Admin 
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }

                //Get Menu By Menu ID 
                var menu = await _unitOfwork. MenuRepository.GetMenuById(MenuDto.MenuId);

                //Check  menu Is Null 
                if (menu == null)
                {
                    Log.Error($"Menu Not Found ");
                    throw new ArgumentNullException("Menu", "Not Found Menu");
                }
                Log.Information("Menu Is  Existing");

                //Update Menu
                menu.Name = MenuDto.Name;
                menu.Description = MenuDto.Description;
                menu.Price = MenuDto.Price;
                menu.IsActive = true;

                //Update Menu And SaveChanges in Database
                await _unitOfwork. MenuRepository.UpdateMenu(menu);
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

        public async Task<string> DeleteMenu(int MenuId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");
                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");
                //Check Is Admin 
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }
                //Get Menu By Menu ID 
                var menu = await _unitOfwork. MenuRepository.GetMenuById(MenuId);
                //Check  menu Is Null 
                if (menu == null)
                {
                    Log.Error($"Menu Not Found ");
                    throw new ArgumentNullException("Menu", "Not Found Menu ");

                }
                Log.Information("Menu Is In Existing");

                //Delete Menu And SaveChanges In Database
                await _unitOfwork. MenuRepository.DeleteMenu(menu);
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
    }
}
