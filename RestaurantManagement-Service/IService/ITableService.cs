

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs.TableDTO;

namespace Restaurants_Service.IService
{
    public interface ITableService
    {

        //Returns are refunded to all Tables
        Task<List<TableCardDTOs>> GetAllTables([FromHeader] string email, [FromHeader] string password);

        //The Table's return is returned to the one whose ID is equal to the Table's ID
        Task<TableCardDTOs> GetTableById(int id, [FromHeader] string email, [FromHeader] string password);

        //Create a new Table
        Task<string> AddTables(CreatTableDTO table, [FromHeader] string email, [FromHeader] string password);

        //Table Update 
        Task<string> UpdateTable(UpdateTableDto table, [FromHeader] string email, [FromHeader] string password);


        //Table Delete 
        Task<string> DeleteTable(int id, [FromHeader] string email, [FromHeader] string password);









    }
}
