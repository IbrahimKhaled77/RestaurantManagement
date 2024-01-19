

using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.TableDTO;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement_Repository.Implementation
{
    public class TableRepository : ITableRepository
    {

        private readonly RestaurantManagementContext _context;

        public TableRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public Task<bool> AddTable(CreatTableDTO table)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> DeleteTable(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<List<TableCardDTOs>> GetAllTables()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<TableCardDTOs> GetTableById(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> UpdateTable(UpdateTableDto table)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
    }
}
