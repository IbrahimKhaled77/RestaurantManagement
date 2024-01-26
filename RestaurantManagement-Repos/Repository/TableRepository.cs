using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Context;
using RestaurantManagement.DTOs.TableDTO;
using RestaurantManagement.Helper.Mapper;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;


namespace RestaurantManagement.Implementation
{
    public class TableRepository : ITableRepository
    {

        private readonly RestaurantManagemenstContext _context;

        public TableRepository(RestaurantManagemenstContext context)
        {
            _context = context;
        }



        // Get All of List Table  Card DTOs
        public async Task<List<TableCardDTOs>> GetAllTables()
        {



            List<TableCardDTOs> Tables =  await _context.Table.Include(t => t.Order).Select(x => new TableCardDTOs{

                        TableId = x.TableId,
                        TableNumber = x.TableNumber,
                        IsActive = x.IsActive,
                        IsActiveOrder = x.IsActiveOrder,
                        Orders = MappingHelper.TableDtoMapper(x.Order.ToList()),

                    }).ToListAsync();

            return Tables;
        }


        // Get Table By Table Id
        public async Task<Table> GetTableByIdOne(int TableId)
        {
            var table1 =  await _context.Table.FindAsync(TableId)!;

            return table1!;
        }


        // I enter a table ID and it returns a table linked to a request table,
        // meaning a table for the one who has a request.
        public async Task<Table> GetTableById(int TableId)
        {
            var table1 = await _context.Table.Include(t => t.Order).FirstOrDefaultAsync(x => x.TableId == TableId);

            return table1!;
        }

        //Add Order in Database in Table Order 
        public async Task AddTables(Table Table)
        {
            _context.Table.Add(Table);
            await _context.SaveChangesAsync();

        }

        //Update OrderItem in Database in Table OrderItem 
        public async Task UpdateTable(Table Table)
        {
            _context.Table.Update(Table);
            await _context.SaveChangesAsync();
        }

        //Delete OrderItem in Database in Table OrderItem 
        public async Task DeleteTable(Table Table)
        {

            _context.Table.Remove(Table);
            await _context.SaveChangesAsync();
        }

    


    }
}
 