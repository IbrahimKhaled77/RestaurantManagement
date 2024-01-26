using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management_Repository.IRepository;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;

namespace RestaurantManagement_Repository.Implementation
{
    public class OrderItemRepository : IOrderItemRepository
    {

        private readonly RestaurantManagementContext _context;

        public OrderItemRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public  async Task<string> AddOrderItem(CreateOrderItemDTO orderItemDTO, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                var isCustomerLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }





                Log.Information("OrderItem Is In Processing");
                var menu= await _context.Menu.FindAsync(orderItemDTO.MenuId);
                var order = await _context.Order.FindAsync(orderItemDTO.OrderId);

                if (menu ==null || order == null)
                {
                    Log.Error($"menu or order Not Found ");
                    throw new InvalidOperationException("Invalid menu or order not Fund");
                }

                OrderItem orderItem = new OrderItem();
                orderItem.MenuId = menu.MenuId;
                orderItem.OrderId = order.OrderId;
                orderItem.Quantity = orderItemDTO.Quantity;
                orderItem.IsActive = true;

                _context.OrderItem.Add(orderItem);
                await _context.SaveChangesAsync();
                Log.Information("orderItem Is Updated");
                Log.Debug($"Debugging AddOrderItem Has been Finised Successfully With Order ID  = {orderItem.OrderItemId}");

                var Order = await _context.Order.FirstOrDefaultAsync(x => x.OrderId == order.OrderId);


                Order.TotalPrice += menu.Price * orderItem.Quantity;

                _context.Order.Update(Order);
                await _context.SaveChangesAsync();
                Log.Information("Order Is Updated");
                Log.Debug($"Debugging UpdateOrder Has been Finised Successfully With Order ID  = {Order.OrderId}");

                return "AddOrderItem Has been Finished Successfully With Order";

           
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"AddOrderItem Not Found: {ex.Message}");
                throw new DbUpdateException($"Database Error: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in the database: {ex.Message}");
                throw new DbUpdateException($"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task<string> DeleteOrderItem(int OrderItemId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {

                var isCustomerLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }



                var OrderItem = await _context.OrderItem.FindAsync(OrderItemId);
                if (OrderItem != null)
                {
                    Log.Information("OrderItem Is  Existing");
                    _context.OrderItem.Remove(OrderItem);
                    await _context.SaveChangesAsync();
                    Log.Information("OrderItem Is Deleted");
                    Log.Debug($"Debugging DeleteOrderItem Has been Finised Successfully With order ID  = {OrderItem.OrderItemId}");

                    return "DeleteOrderItem Has been Finised Successfully";
                }
                else
                {

                    throw new ArgumentNullException("OrderItem", "Not Found OrderItem");
                }


            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"OrderItem Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {

                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");

            }
        }


        public async Task<List<OrderItemCardDTO>> GetAllOrderItems([FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                var isCustomerLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }



                var Order = await _context.OrderItem.Select(Order1 => new OrderItemCardDTO
                {
                    OrderItemId = Order1.OrderItemId,
                    OrderId = Order1.OrderId,
                    MenuId = Order1.MenuId,
                    Quantity = Order1.Quantity,
                    IsActive = Order1.IsActive


                }).ToListAsync();
                Log.Information("OrderItem Is Reached");
                Log.Debug($"Debugging Get OrderItem By Id Has been Finised Successfully");


                return Order;



            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"OrderItem Not Found: {ex.Message}");
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

        public async Task<OrderItemCardDTO> GetOrderItemById(int OrderItemId, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                var isCustomerLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }


                var OrderItemId1 = await _context.OrderItem.FirstOrDefaultAsync(x => x.OrderItemId == OrderItemId);
                if (OrderItemId1 != null)
                {
                    Log.Information($"OrderItemId Is  Existing: {OrderItemId1.OrderId}");

                    OrderItemCardDTO OrderItemCardDTO = new OrderItemCardDTO();
                    OrderItemCardDTO.OrderItemId = OrderItemId1.OrderItemId;
                    OrderItemCardDTO.OrderId = OrderItemId1.OrderId;
                    OrderItemCardDTO.MenuId = OrderItemId1.MenuId;
                    OrderItemCardDTO.Quantity = OrderItemId1.Quantity;
                    OrderItemCardDTO.IsActive = OrderItemId1.IsActive;
                   
                    Log.Information("OrderItemId Is Reached");
                    Log.Debug($"Debugging Get OrderItemId By Id Has been Finised Successfully With Order ID  = {OrderItemCardDTO.OrderItemId}");
                    return OrderItemCardDTO;


                }
                else
                {
                    Log.Error($"OrderItemId Not Found ");
                    throw new ArgumentNullException("Order1", "Not Found");

                }
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"OrderItemId Not Found: {ex.Message}");
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

        public async Task<string> UpdateOrderItem(UpdateOrderIterm OrderItermDto, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {

              
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }




                var OrderItem = await _context.OrderItem.FirstOrDefaultAsync(x => x.OrderItemId == OrderItermDto.OrderItemId);
                if (OrderItem == null)
                {
                    Log.Error($"OrderItem Not Found ");
                    throw new ArgumentNullException("OrderItem", "Not Found OrderItem");

                }
                var menu= await _context.Menu.FindAsync(OrderItermDto.MenuId);
                var Order = await _context.Order.FindAsync(OrderItermDto.OrderId);
                Log.Information("OrderItem Is  Existing");
                OrderItem.MenuId = menu.MenuId;
                OrderItem.OrderId = Order.OrderId;
                OrderItem.IsActive = OrderItermDto.IsAcitvite;
                OrderItem.Quantity = OrderItermDto.Quantity;


                _context.OrderItem.Update(OrderItem);
                await _context.SaveChangesAsync();
                Log.Information("OrderItem Is Updated");
                Log.Debug($"Debugging UpdateOrderItem Has been Finised Successfully With Order ID  = {OrderItem.OrderItemId}");

                Order.TotalPrice += menu.Price * OrderItem.Quantity;

                _context.Order.Update(Order);
                await _context.SaveChangesAsync();
                Log.Information("Order Is Updated");
                Log.Debug($"Debugging UpdateOrder Has been Finised Successfully With Order ID  = {Order.OrderId}");

              

                return "UpdateOrderItem Has been Finised Successfully";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"OrderItem Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {

                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");

            }
        }

    }
}
