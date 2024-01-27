

using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Context;
using RestaurantManagement.DTOs.OrderItemDTO;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Repository
{
    public class OrderItemRepository :IOrderItemRepository
    {

        private readonly RestaurantManagemenstContext _context;

        public OrderItemRepository(RestaurantManagemenstContext context)
        {
            _context = context;
        }

        // Get All of List Order Item Card DTO
        public async Task<List<OrderItemCardDTO>> GetAllOrderItems()
        {

            List<OrderItemCardDTO> OrderItemCardDTO = await _context.OrderItem.Include(x=>x.Menu).ThenInclude(x=>x.OrderItems).ThenInclude(x=>x.Order).Select(Order1 => new OrderItemCardDTO
            {


                OrderItemId = Order1.OrderItemId,
                MenuId = Order1.MenuId,
                MenuName = Order1.Menu.Name,
                price = Order1.Menu.Price,
                OrderId = Order1.OrderId,
                Quantity = Order1.Quantity,
                IsActive = Order1.IsActive


            }).ToListAsync();

            return OrderItemCardDTO;

        }

        // Get OrderItem By OrderItem Id
        public async Task<OrderItem> GetOrderItemById(int OrderItemId)
        {
            var orderItem = await _context.OrderItem.Include(x => x.Menu).ThenInclude(x => x.OrderItems).ThenInclude(x => x.Order).FirstOrDefaultAsync(x => x.OrderItemId == OrderItemId);

            return orderItem!;
        }

        //Add OrderItem in Database in Table OrderItem 
        public async Task AddOrderItem(OrderItem OrderItem)
        {
            _context.OrderItem.Add(OrderItem);
            await _context.SaveChangesAsync();
        }

        //Update OrderItem in Database in Table OrderItem 
        public async Task UpdateOrderItem(OrderItem OrderItem)
        {
            _context.OrderItem.Update(OrderItem);
            await _context.SaveChangesAsync();

        }

        //Delete OrderItem in Database in Table OrderItem 
        public async Task DeleteOrderItem(OrderItem OrderItem)
        {
            _context.OrderItem.Remove(OrderItem);
            await _context.SaveChangesAsync();
        }

     




   




    }
}
