


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.TableDTO;
using RestaurantManagement_Repository.Helper.Mapper;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;

namespace RestaurantManagement_Repository.Implementation
{
    public class TableRepository : ITableRepository
    {

        private readonly RestaurantManagementContext _context;

        public TableRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        //GetAllTables
        public async Task<List<TableCardDTOs>> GetAllTables([FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                var isCustomerLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {
                    throw new Exception("You Must Login In To Your Account");

                }


                var Tables = await _context.Table.Include(t => t.Order)
                    .Select(x => new TableCardDTOs
                    {

                        TableId = x.TableId,
                        TableNumber = x.TableNumber,
                        IsActive = x.IsActive,
                        IsActiveOrder = x.IsActiveOrder,
                          Orders = MappingHelper.TableDtoMapper(x.Order.ToList()),

                    }).ToListAsync();
                Log.Information("Tables are Reached");
                Log.Debug($"Debugging GetAllTables Has been Finised Successfully");

                return Tables;





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

        //GetTableById
        public async Task<TableCardDTOs> GetTableById(int id,[FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                var isCustomerLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {
                    throw new Exception("You Must Login In To Your Account");

                }



                var table1 = await _context.Table.Include(t => t.Order)
                    .FirstOrDefaultAsync(x => x.TableId == id);

                if (table1 != null)
                {
                    Log.Information($"Table Is  Existing: {table1.TableId}");

                    TableCardDTOs tableCardDTOs = new TableCardDTOs();
                    tableCardDTOs.TableId = table1.TableId;
                    tableCardDTOs.TableNumber = table1.TableNumber;
                    tableCardDTOs.IsActiveOrder = table1.IsActiveOrder;
                    tableCardDTOs.IsActive = table1.IsActive;
                    tableCardDTOs.Orders = MappingHelper.TableDtoMapper(table1.Order.ToList());
                    Log.Information("Table Is Reached");
                    Log.Debug($"Debugging GetTableById Has been Finised Successfully With Table ID  = {table1.TableId}");
                    return tableCardDTOs;
                   

                }
                else
                {
                    Log.Error($"Table Not Found ");
                    throw new ArgumentNullException("table", "Not Found");

                }

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Table Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");
                
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception : {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
               
            }
        }


        //CreatTable
        public async Task<string> AddTables(CreatTableDTO table, [FromHeader] string email, [FromHeader] string password)
        {
        

            try
            {
                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin" && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {
                    throw new Exception("You Must Login In To Your Account");
                    
                }
                var isAdmin = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin");
                if (!isAdmin)
                {
                    
                    throw new Exception("You Don't have the required Permission");
                }



                Log.Information("Table Is In Procesing");
                var table1 = new Table();
                table1.TableNumber = table.TableNumber;
                table1.IsActive = true;
                table1.IsActiveOrder = false;


                _context.Table.Add(table1);
                Log.Information("Table Is In Finised");
                Log.Debug($"Debugging AddTables Has been Finised Successfully With Table ID  {table1.TableId}");
                await _context.SaveChangesAsync();
         
                return "AddTables Has been Finised Successfully";

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }   catch (ArgumentNullException ex)
            {
                Log.Error($"Table Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        //UpdateTable
        public async Task<string> UpdateTable(UpdateTableDto table,[FromHeader] string email, [FromHeader] string password)
        {


            try
            {
                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin" && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {
                    return "You Must Login In To Your Account";
                    throw new Exception("You Must Login In To Your Account");
                }
                var isAdmin = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin");
                if (!isAdmin)
                {
                    return "You Don't have the required Permission";
                    throw new Exception("You Don't have the required Permission");
                }

                var table1 = await _context.Table.FindAsync(table.TableId);
                //check if the element Exist
                if (table1 != null)
                {
                    Log.Information("Table Is  Existing");
                    table1.TableNumber = table.TableNumber;
                    table1.IsActive = table1.IsActive;
                    _context.Table.Update(table1);  
                    await _context.SaveChangesAsync();
                    Log.Information("Table Is Updated");
                    Log.Debug($"Debugging UpdateTable Has been Finised Successfully With Table ID  = {table1.TableId}");

                    return "UpdateTable Has been Finised Successfully";

                }
                else
                {
                    Log.Error($"Table Not Found ");
                    throw new ArgumentNullException("table", "Not Found table");

                }


            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Table Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }

            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");
              
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
               
            }
        }

        //DeleteTable
      
        public async  Task<string> DeleteTable(int id, [FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {
                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin" && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {
                    return "You Must Login In To Your Account";
                    throw new Exception("You Must Login In To Your Account");
                }
                var isAdmin = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin");
                if (!isAdmin)
                {
                    return "You Don't have the required Permission";
                    throw new Exception("You Don't have the required Permission");
                }


                var  table = await _context.Table.FindAsync(id);
                if (table != null)
                {
                    Log.Information("Table Is In Existing");
                    _context.Table.Remove(table);
                    await _context.SaveChangesAsync();
                    Log.Information("Table Is Deleted");
                    Log.Debug($"Debugging DeleteTable Has been Finised Successfully With Table ID  = {table.TableId}");

                    return "DeleteTable Has been Finised Successfully";
                }
                else {
                    Log.Error($"Table Not Found ");
                    throw new ArgumentNullException("table", "Not Found");
                }




            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Table Not Found: {ex.Message}");
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
 