using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.TableDTO;
using RestaurantManagement.Helper.Mapper;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Service.IService;
using Serilog;

namespace Restaurants_Service.Service
{
    public class TableService:ITableService
    {
        private readonly IUnitOfwork _unitOfwork;
        public TableService(IUnitOfwork unitOfwork ) {
            _unitOfwork = unitOfwork;
            
        }

      
        //GetAllTables
        public async Task<List<TableCardDTOs>> GetAllTables([FromHeader] string email, [FromHeader] string password)
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

                // Get All of List Tables 
                var Tables = await _unitOfwork.TableRepository.GetAllTables();

                //Check is Tables equal null
                if (Tables == null)
                {
                    Log.Error($"Tables Not Found ");
                    throw new ArgumentNullException("Tables", "Not Found Tables");

                }

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
        public async Task<TableCardDTOs> GetTableById(int TableId, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {  
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork. CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {
                    throw new Exception("You Must Login In To Your Account");

                }


                // Get Table By TableID 
                var table1 = await _unitOfwork.TableRepository.GetTableById(TableId);

                //Check Table is Not  Null
                if (table1 != null)
                {
                    Log.Information($"Table Is  Existing: {table1.TableId}");

                    //Create Table Card DTOs
                    TableCardDTOs tableCardDTOs = new TableCardDTOs() {
                        TableId = table1.TableId,
                    TableNumber = table1.TableNumber,
                    IsActiveOrder = table1.IsActiveOrder,
                    IsActive = table1.IsActive,
                    Orders = MappingHelper.TableDtoMapper(table1.Order.ToList()),

                };
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
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");
                //Check isAdminLoggedIn Not Found
                if (!isEmployeeLoggedIn)
                {
                    throw new Exception("You Must Login In To Your Account");

                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");
                //Check Is Admin Not Found
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }



                Log.Information("Table Is In Procesing");
                //Create a New Table
                var table1 = new Table()
                {
                   TableNumber = table.TableNumber,
                    IsActive = true,
                    IsActiveOrder = false
                };

                //Add Table And SaveChanges In Database
                await _unitOfwork.TableRepository.AddTables(table1);

                Log.Information("Table Is In Finised");
                Log.Debug($"Debugging AddTables Has been Finised Successfully With Table ID  {table1.TableId}");
         
         
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
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");
                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {
                    return "You Must Login In To Your Account";
                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");
                //Check Is Admin Not Found
                if (!isAdmin)
                {
                    return "You Don't have the required Permission";
                    throw new Exception("You Don't have the required Permission");
                }
                //Get Table By Table ID 
                var table1 = await _unitOfwork. TableRepository.GetTableById(table.TableId);
                //check if the Table Exist
                if (table1 != null)
                {
                    Log.Information("Table Is  Existing");


                    //Update Table
                    table1.TableNumber = table.TableNumber;
                    table1.IsActive = table1.IsActive;

                    //Update Table And SaveChanges in Database
                    await _unitOfwork. TableRepository.UpdateTable(table1);


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
        public async  Task<string> DeleteTable(int TableId, [FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");
                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {
                    return "You Must Login In To Your Account";
                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");
                //Check Is Admin Not Found
                if (!isAdmin)
                {
                    return "You Don't have the required Permission";
                    throw new Exception("You Don't have the required Permission");
                }

                //Get Table By Table ID 
                var table = await _unitOfwork. TableRepository.GetTableById(TableId);
                //check if the Table Exist
                if (table != null)
                {
                    Log.Information("Table Is In Existing");

                    //Delete  Table And SaveChanges IN Database
                    await _unitOfwork. TableRepository.DeleteTable(table);


                    Log.Information("Table Is Deleted");
                    Log.Debug($"Debugging DeleteTable Has been Finised Successfully With Table ID  = {table.TableId}");

                    return "DeleteTable Has been Finised Successfully";
                }
                //check if the Table Is Null 
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
