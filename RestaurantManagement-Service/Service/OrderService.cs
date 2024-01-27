

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.OrderDTO;
using RestaurantManagement.Helper.Mapper;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Service.IService;
using Serilog;

namespace Restaurants_Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfwork _unitOfwork;
        public OrderService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        
     
        }
    
    


        public async Task<List<OrderCardDTO>> GetAllOrders([FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                // Get All of List Orders 
                var Orders = await _unitOfwork.OrderRepository.GetAllOrders();

                //Check is Orders equal null
                if (Orders == null)
                {
                    Log.Error($"Orders Not Found ");
                    throw new ArgumentNullException("Orders", "Not Found Orders");

                }

                Log.Information("Order Is Reached");
                Log.Debug($"Debugging Get Order By Id Has been Finised Successfully");


                return Orders;



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


        public async Task<OrderCardDTO> GetOrderById(int OrderId, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork. CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                //Get Order By OrderId
                var Order1 = await _unitOfwork.OrderRepository.GetOrderById(OrderId);
                //Check Order is not  null
                if (Order1 != null)
                {
                    Log.Information($"Order Is  Existing: {Order1.OrderId}");

                    //Create Order Card DTO
                    OrderCardDTO orderCardDTO = new OrderCardDTO() {
                     OrderId = Order1.OrderId,
                    TableNumber = Order1.TableNumber,
                    TotalPrice = Order1.TotalPrice,
                    IsActive = Order1.IsActive,
                    EmployeeOrder = MappingHelper.EmployeeOrderDtoMapper(Order1.EmployeeOrder!.ToList()),
                    OrderItems = MappingHelper.OrderItemDtoMapper(Order1.OrderItems!.ToList()),
                    
                };
                    Log.Information("Order Is Reached");
                    Log.Debug($"Debugging Get Order By Id Has been Finised Successfully With Order ID  = {Order1.OrderId}");

                    return orderCardDTO;




                }
                else    //Check Order is null
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

        public  async Task<string> AddOrder(CreatOrderDTO orderDto,[FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }






                Log.Information("Order Is In Processing");

                var order1 = new Order();

               
            
                //Get Customer by CustomerId  is Equal orderDto.CustomerId
                order1.Customer = await _unitOfwork.CustomerRepository.GetCustomerById(orderDto.CustomerId);
                //Get Table by TablerId  is Equal orderDto.TableId
                order1.Table = await _unitOfwork.TableRepository.GetTableById(orderDto.TableId);
                
               

                //Check Table And Customer Is null
                if (order1.Table == null && order1.Customer == null)
                {

                    throw new InvalidOperationException($"Table {orderDto.TableId} OR Customer {orderDto.CustomerId} are null.");
                   
                }
                //The table is Not  IsActiveOrder
                else if (order1.Table!.IsActiveOrder) {

                    throw new InvalidOperationException("The Table is not active.");
                }
                order1.TableNumber = order1.Table.TableNumber;
                order1.IsActive = true;
                order1.TotalPrice = 0;
                order1.Table.IsActiveOrder = true;
                //Add Order And SaveChanges In Database
                await _unitOfwork.OrderRepository.AddOrder(order1);

                var table1 = new Table();
                //The table is now reserved
               

             

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


        public async Task<string> UpdateOrder(UpdateOrderDTO OrderDto, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {

                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }


                //Get Order By OrderID
                var Order = await _unitOfwork. OrderRepository.GetOrderById(OrderDto.OrderId);
                //Check  Order is not Found
                if (Order == null)
                {
                    Log.Error($"Order Not Found ");
                    throw new ArgumentNullException("Order", "Not Found Order");

                }
                Log.Information("Order Is  Existing");

                //Update Order
                Order.Table = await _unitOfwork. TableRepository.GetTableById(OrderDto.TableId);
                Order.TableNumber = Order.Table.TableId;
                Order.TotalPrice = 0;
                Order.IsActive = OrderDto.IsActive;

                //Updata Order And SaveChanges In Database
                await _unitOfwork.OrderRepository.UpdateOrder(Order);
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

        public async Task<string> DeleteOrder(int OrderId, [FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {


                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");
                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {
                    return "You Must Login In To Your Account";
                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");
                //Check Is Admin Not Found
                if (!isAdmin)
                {
                    return "You Don't have the required Permission";
                    throw new Exception("You Don't have the required Permission");
                }

                //Get Order By OrderID
                var order =await _unitOfwork. OrderRepository.GetOrderById(OrderId);

                //Check Order Is Found
                if (order != null)
                {
                    Log.Information("Table Is  Existing");



                    //DeleteOrder And Save Changes In Database
                    await _unitOfwork. OrderRepository.DeleteOrder(order);
                    
                    var Table = new Table();
                    //The reservation is invalidated
                    Table.IsActiveOrder = false;

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

         

    }
}
