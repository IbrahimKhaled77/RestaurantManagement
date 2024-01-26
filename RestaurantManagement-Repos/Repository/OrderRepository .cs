using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Context;
using RestaurantManagement.DTOs.OrderDTO;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.Helper.Mapper;

namespace RestaurantManagement_Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly RestaurantManagemenstContext _context;

        public OrderRepository(RestaurantManagemenstContext context)
        {
            _context = context;
        }


        // Get All of List Order  Card DTO
        public async Task<List<OrderCardDTO>> GetAllOrders()
        {

        List<OrderCardDTO> Orders = await _context.Order.Select(Order1 => new OrderCardDTO
        {
            OrderId = Order1.OrderId,
            TableNumber = Order1.TableNumber,
            IsActive = Order1.IsActive,
            EmployeeOrder = MappingHelper.EmployeeOrderDtoMapper(Order1.EmployeeOrder!.ToList()),
            OrderItems = MappingHelper.OrderItemDtoMapper(Order1.OrderItems!.ToList()),
            TotalPrice=Order1.TotalPrice,

        }).ToListAsync();


            return Orders;

        }

        // Get Order By Order Id
        public async Task<Order> GetOrderById(int OrderId)
        {

          var order =  await _context.Order.Where(x=>x.OrderId==OrderId).Include(t => t.EmployeeOrder).Include(t => t.OrderItems).SingleAsync();

            return order;
        }

        //Add Order in Database in Table Order 
        public async Task AddOrder(Order Order)
        {
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

        }

        //Update OrderItem in Database in Table OrderItem 
        public async Task UpdateOrder(Order Order)
        {
            _context.Order.Update(Order);
            await _context.SaveChangesAsync();
        }

        //Delete OrderItem in Database in Table OrderItem 
        public async Task DeleteOrder(Order Order)
        {

           

            _context.Order.Remove(Order);
            await _context.SaveChangesAsync();
        }

      

        //Save any changes made
        public async Task SaveChangesAsync()
        {

            await _context.SaveChangesAsync();
        }



    }
}
