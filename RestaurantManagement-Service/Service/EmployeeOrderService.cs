using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.EmployeeOrderCardDTO;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Service.IService;
using Serilog;

namespace Restaurants_Service.Service
{
    public class EmployeeOrderService : IEmployeeOrderService
    {
        private readonly IUnitOfwork _unitOfwork;
        public EmployeeOrderService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
          
        }

        //Add the employees who will execute the order
        public async Task<string> AddEmployeeOrder(EmployeeOrderCreatDTOs EmployeeOrderDto, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {

                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn =  await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");

                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin =  await _unitOfwork.EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");

                //Check Is Admin 
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }

                Log.Information("EmployeeOrder Is In Procesing");
                //Create New EmployeeOrder 
                var newEmployeeOrder = new EmployeeOrder
                {
                    EmployeeId = EmployeeOrderDto.EmployeeId,
                    OrderId = EmployeeOrderDto.OrderId,
                };

                //Add EmployeeOrder and  SaveChange in database  
                await _unitOfwork.EmployeeOrderRepository.AddEmployeeOrder(newEmployeeOrder);

                Log.Information("EmployeeOrder Is In Finised");
                Log.Debug($"Debugging AddEmployeeOrder Has been Finised Successfully EmployeeOrder ID  {newEmployeeOrder.EmployeeOrderId} ");

                //Get Employee By EmployeeID
                var employee = await _unitOfwork. EmployeeRepository.GetEmployeeById(EmployeeOrderDto.EmployeeId);

                //Get order By orderID
                var order = await _unitOfwork.OrderRepository.GetOrderById(EmployeeOrderDto.OrderId);

                // Check if the Employee and Order exist
                if (employee == null || order == null)
                {
                    throw new ArgumentNullException("EmployeeOrder", "Employee or Order not found.");
                }

                employee.EmployeeOrder ??= new List<EmployeeOrder>();
                order.EmployeeOrder ??= new List<EmployeeOrder>();

                employee.EmployeeOrder.Add(newEmployeeOrder);
                order.EmployeeOrder.Add(newEmployeeOrder);

                await _unitOfwork. EmployeeOrderRepository.SaveChangesAsync();

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
