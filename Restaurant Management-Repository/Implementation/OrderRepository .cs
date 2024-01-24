


using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.OrderDTO;

using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using RestaurantManagement_Repository.Helper;
using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using Serilog;
using RestaurantManagement_Repository.Helper.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement_Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly RestaurantManagementContext _context;

        public OrderRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public  async Task<string> AddOrder(CreatOrderDTO order,[FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin" && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {
                
                    throw new Exception("You Must Login In To Your Account");
                }
                var isAdmin = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin");
                if (!isAdmin)
                {
                  
                    throw new Exception("You Don't have the required Permission");
                }




                Log.Information("Order Is In Processing");
                var order1 = new Order();
                order1.Customer = await _context.Customer.FindAsync(order.CustomerId);
                order1.Table = await _context.Table.FindAsync(order.TableId);
                order1.TableNumber = order1.Table.TableNumber;
                order1.TotalPrice = 0;
                order1.IsActive = true;

                _context.Order.Add(order1);
                await _context.SaveChangesAsync();

               

               order1.OrderItems = new List<OrderItem>();

                foreach (var orderItemDTO in order.OrderItems)
                {
                    var menu = await _context.Menu.FindAsync(orderItemDTO.MenuId);
                    menu.OrderItems ??= new List<OrderItem>();

                    if (menu == null)
                    {
                        throw new ArgumentNullException("Menu", "Menu not found");
                    }
                    Log.Information("OrderItem Is In Processing");
                    OrderItem orderItem = new OrderItem
                    {
                        MenuId = orderItemDTO.MenuId,
                        OrderId = order1.OrderId,
                        Quantity = orderItemDTO.Quantity,
                        IsActive = true,
                    };

                    _context.OrderItem.Add(orderItem);
                    await _context.SaveChangesAsync(); // Save each OrderItem individually
                    Log.Information("orderItem Is In Finished");
                    Log.Debug($"Debugging orderItem Has been Finished Successfully With orderItem ID {orderItem.OrderItemId}");

                    order1.OrderItems.Add(orderItem);
                    menu.OrderItems.Add(orderItem);
                }

                // Save changes once all OrderItems are added
                await _context.SaveChangesAsync();

                // Calculate TotalPrice after OrderItems are saved
                order1.TotalPrice = order1.OrderItems.Sum(oi => oi.Quantity * oi.Menu.Price);

                // Update the Order entity with the calculated TotalPrice
                _context.Order.Update(order1);
                await _context.SaveChangesAsync();


                return "AddOrder Has been Finished Successfully With Order";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Order Not Found: {ex.Message}");
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


        public async Task<string> DeleteOrder(int id, [FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {

                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin" && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {
                  
                    throw new Exception("You Must Login In To Your Account");
                }
                var isAdmin = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin");
                if (!isAdmin)
                {
                    
                    throw new Exception("You Don't have the required Permission");
                }



                var order =await _context.Order.FindAsync(id);
                if (order != null)
                {
                    Log.Information("Table Is  Existing");
                    _context.Order.Remove(order);
                    await _context.SaveChangesAsync();
                    Log.Information("Table Is Deleted");
                    Log.Debug($"Debugging DeleteOrder Has been Finised Successfully With order ID  = {order.OrderId}");
                    return "DeleteTable Has been Finised Successfully";
                }
                else {

                    throw new ArgumentNullException("Order", "Not Found Order");
                }


            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Order Not Found: {ex.Message}");
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

        public async Task<List<OrderCardDTO>> GetAllOrders([FromHeader] string email, [FromHeader] string password)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Order Not Found: {ex.Message}");
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

        public async Task<OrderCardDTO> GetOrderById(int OrderId,[FromHeader] string email, [FromHeader] string password)
        {
             
            try
            {
                var isCustomerLoggedIn = await _context.Customer.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                var isEmployeeLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                var Order1 = await _context.Order.Include(t => t.EmployeeOrder)
                   .FirstOrDefaultAsync(x => x.OrderId == OrderId);
                if (Order1 != null)
                {
                    Log.Information($"Order Is  Existing: {Order1.OrderId}");

                    OrderCardDTO orderCardDTO = new OrderCardDTO();
                    orderCardDTO.OrderId = Order1.OrderId;
                    orderCardDTO.TableNumber = Order1.TableNumber;
                    orderCardDTO.IsActive = Order1.IsActive;
                    orderCardDTO.EmployeeOrder = TableMappingHelper.EmployeeOrderDtoMapper(Order1.EmployeeOrder.ToList());
                    Log.Information("Order Is Reached");
                    Log.Debug($"Debugging Get Order By Id Has been Finised Successfully With Order ID  = {Order1.OrderId}");
                    return orderCardDTO;


                }
                else
                {
                    Log.Error($"Order Not Found ");
                    throw new ArgumentNullException("Order1", "Not Found");

                }
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Order Not Found: {ex.Message}");
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

        public  async Task<string> UpdateOrder(UpdateOrderDTO OrderDto, [FromHeader] string email, [FromHeader] string password)
        {
           
            try
            {

                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin" && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {
                    
                    throw new Exception("You Must Login In To Your Account");
                }
                var isAdmin = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.Position == "Admin");
                if (!isAdmin)
                {
                   
                    throw new Exception("You Don't have the required Permission");
                }



                var Order = await _context.Order.FirstOrDefaultAsync(x=>x.OrderId== OrderDto.OrderId);
                if (Order == null)
                {
                    Log.Error($"Order Not Found ");
                    throw new ArgumentNullException("Order", "Not Found Order");

                }
                Log.Information("Order Is  Existing");
                Order.Table = await _context.Table.FindAsync(OrderDto.TableId);
                Order.TableNumber = Order.Table.TableId;
                Order.TotalPrice = OrderDto.TotalPrice;
                Order.IsActive = OrderDto.IsActive;


                _context.Order.Update(Order);
                await _context.SaveChangesAsync();
                Log.Information("Order Is Updated");
                Log.Debug($"Debugging UpdateOrder Has been Finised Successfully With Order ID  = {Order.OrderId}");

                return "UpdateOrder Has been Finised Successfully";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Order Not Found: {ex.Message}");
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
