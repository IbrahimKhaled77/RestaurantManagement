using RestaurantManagement.DTOs.TableDTO;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface ITableRepository
    {
        Task<List<TableCardDTOs>> GetAllTables();
        Task<Table> GetTableById(int TableId);

        Task<Table> GetTableByIdOne(int TableId);

        Task AddTables(Table Menu);

        Task UpdateTable(Table Menu);

        Task DeleteTable(Table Menu);

    }
}
