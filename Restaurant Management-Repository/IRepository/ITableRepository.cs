

using RestaurantManagement_Repository.DTOs.TableDTO;

namespace RestaurantManagement_Repository.IRepository
{
    public interface ITableRepository
    {

        Task<List<TableCardDTOs>> GetAllTables();
        Task<TableCardDTOs> GetTableById(int id);
        Task<string> AddTables(CreatTableDTO table);
        Task<string> UpdateTable(UpdateTableDto table);
        Task<string> DeleteTable(int id);
    }
}
