

using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.DTOs.TableDTO;

namespace RestaurantManagement_Repository.IRepository
{
    public interface ITableRepository
    {

        
        Task<List<TableCardDTOs>> GetAllTables([FromHeader] string email, [FromHeader] string password);

        
        Task<TableCardDTOs> GetTableById(int id, [FromHeader] string email, [FromHeader] string password);
        
        //Admin
        Task<string> AddTables(CreatTableDTO table ,  [FromHeader] string email, [FromHeader] string password);
        //Admin
        Task<string> UpdateTable(UpdateTableDto table , [FromHeader] string email, [FromHeader] string password);

        //Admin
        Task<string> DeleteTable(int id ,[FromHeader] string email, [FromHeader] string password);
    }
}
