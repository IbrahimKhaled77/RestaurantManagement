
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.EmployeeOrderCardDTO;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;

namespace RestaurantManagement_Repository.Implementation
{
    public class EmployeeOrdersRepository : IEmployeeOrderRepository
    {
        private readonly RestaurantManagementContext _context;

        public EmployeeOrdersRepository(RestaurantManagementContext context)
        {
            _context = context;
        }

        public async Task<string> AddEmployeeOrder(EmployeeOrderCreatDTOs EmployeeOrderDto, [FromHeader] string email, [FromHeader] string password)
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

                Log.Information("EmployeeOrder Is In Procesing");
                var newEmployeeOrder = new EmployeeOrder
                {
                 EmployeeId = EmployeeOrderDto.EmployeeId,
                 OrderId = EmployeeOrderDto.OrderId,
                };

                _context.EmployeeOrder.Add(newEmployeeOrder);
                await _context.SaveChangesAsync();
                Log.Information("EmployeeOrder Is In Finised");
                Log.Debug($"Debugging AddEmployeeOrder Has been Finised Successfully EmployeeOrder ID  {newEmployeeOrder.EmployeeOrderId} ");
                var employee = await _context.Employee.FindAsync(EmployeeOrderDto.EmployeeId);
                var order = await _context.Order.FindAsync(EmployeeOrderDto.OrderId);
               
                // Check if the Employee and Order exist
                if (employee == null || order == null)
                {
                    throw new ArgumentNullException("EmployeeOrder", "Employee or Order not found.");
                }
               
                employee.EmployeeOrder ??= new List<EmployeeOrder>();
                order.EmployeeOrder ??= new List<EmployeeOrder>();
               
                employee.EmployeeOrder.Add(newEmployeeOrder);
                order.EmployeeOrder.Add(newEmployeeOrder);
               
                await _context.SaveChangesAsync();

                return "AddEmployeeOrder Has been Finised Successfully";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"EmployeeOrder Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }
    }
}
