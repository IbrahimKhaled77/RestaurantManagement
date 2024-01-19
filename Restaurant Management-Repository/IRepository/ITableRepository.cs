

using RestaurantManagement_Repository.DTOs.TableDTO;

namespace RestaurantManagement_Repository.IRepository
{
    public interface ITableRepository
    {

        Task<List<TableCardDTOs>> GetAllTables();
        Task<TableCardDTOs> GetTableById(int id);
        Task<bool> AddTable(CreatTableDTO table);
        Task<bool> UpdateTable(UpdateTableDto table);
        Task<bool> DeleteTable(int id);
    }
}
