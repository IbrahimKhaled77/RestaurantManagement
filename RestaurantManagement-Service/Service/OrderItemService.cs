


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.OrderItemDTO;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Service.IService;
using Serilog;

namespace Restaurants_Service.Service
{
    public class OrderItemService : IOrderItemService
    {


        private readonly IUnitOfwork _unitOfwork;
        public OrderItemService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;


        }

        public async Task<List<OrderItemCardDTO>> GetAllOrderItems([FromHeader] string email, [FromHeader] string password)
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

                // Get All of List OrderItems 
                var Order = await _unitOfwork.OrderItemRepository.GetAllOrderItems();

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
                //Searches for the customerId in the Customer table. The customer must be logged in
                var isCustomerLoggedIn = await _unitOfwork.CustomerRepository.IsCustomerLoggedIn(email, password);
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //check if the isCustomerLoggedIn AND isEmployeeLoggedIn not equal null
                if (!isCustomerLoggedIn && !isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                //Get OrderItem By OrderItemId
                var OrderItemId1 = await _unitOfwork.OrderItemRepository.GetOrderItemById(OrderItemId);
                //Check OrderItem is not  null
                if (OrderItemId1 != null)
                {
                    Log.Information($"OrderItemId Is  Existing: {OrderItemId1.OrderId}");
                    //Create OrderItem Card DTO
                    OrderItemCardDTO OrderItemCardDTO = new OrderItemCardDTO() {
                     OrderItemId = OrderItemId1.OrderItemId,
                     OrderId = OrderItemId1.OrderId,
                     MenuId = OrderItemId1.MenuId,
                     MenuName= OrderItemId1.Menu.Name,
                     price= OrderItemId1.Menu.Price,
                     Quantity = OrderItemId1.Quantity,
                     IsActive = OrderItemId1.IsActive,
                };

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

        public async Task<string> AddOrderItem(CreateOrderItemDTO orderItemDTO, [FromHeader] string email, [FromHeader] string password)
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



                Log.Information("OrderItem Is In Processing");
                //Get Menu By MenuId Is Found
                var menu = await _unitOfwork.MenuRepository.GetMenuById(orderItemDTO.MenuId);
                //Get order By orderId Is Found
                var order = await _unitOfwork.OrderRepository.GetOrderById(orderItemDTO.OrderId);

                //Check Menu Is Found OR Order Is Found
                if (menu == null || order == null)
                {
                    Log.Error($"menu or order Not Found ");
                    throw new InvalidOperationException("Invalid menu or order not Fund");
                }

                //Creat a new OrderItem
                OrderItem orderItem = new OrderItem();
                orderItem.MenuId = menu.MenuId;
                orderItem.OrderId = order.OrderId;
                orderItem.Quantity = orderItemDTO.Quantity;
                orderItem.IsActive = true;

                //Add OrderItem And SaveChanges In database
                await _unitOfwork.OrderItemRepository.AddOrderItem(orderItem);

                Log.Information("orderItem Is Updated");
                Log.Debug($"Debugging AddOrderItem Has been Finised Successfully With Order ID  = {orderItem.OrderItemId}");

                //Get Order By OrderId IS Found 
                var Order = await _unitOfwork.OrderRepository.GetOrderById(order.OrderId);

                //Calculate the total price 
                Order.TotalPrice += menu.Price * orderItem.Quantity;

                //Update Order And SaveChanges In Database
                await _unitOfwork.OrderRepository.UpdateOrder(order);

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
       

        public async Task<string> UpdateOrderItem(UpdateOrderIterm OrderItermDto, [FromHeader] string email, [FromHeader] string password)
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


                var OrderItem = await _unitOfwork.OrderItemRepository.GetOrderItemById(OrderItermDto.OrderItemId);
                if (OrderItem == null)
                {
                    Log.Error($"OrderItem Not Found ");
                    throw new ArgumentNullException("OrderItem", "Not Found OrderItem");

                }
                //Get Menu By MenuId Is Found
                var menu = await _unitOfwork.MenuRepository.GetMenuById(OrderItermDto.MenuId);
                //Get order By orderId Is Found
                var Order = await _unitOfwork.OrderRepository.GetOrderById(OrderItermDto.OrderId);

                Log.Information("OrderItem Is  Existing");

                //Update orderItem
                OrderItem.MenuId = menu.MenuId;
                OrderItem.OrderId = Order.OrderId;
                OrderItem.IsActive = OrderItermDto.IsAcit;
                OrderItem.Quantity = OrderItermDto.Quantity;

                //Update OrderItem And SaveChanges In database
                await _unitOfwork.OrderItemRepository.UpdateOrderItem(OrderItem);
                Log.Information("OrderItem Is Updated");
                Log.Debug($"Debugging UpdateOrderItem Has been Finised Successfully With Order ID  = {OrderItem.OrderItemId}");

                //Calculate the total price 
                Order.TotalPrice += menu.Price * OrderItem.Quantity;

                //Update Order And SaveChanges In database
                await _unitOfwork.OrderRepository.UpdateOrder(Order);

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


        public async Task<string> DeleteOrderItem(int OrderItemId, [FromHeader] string email, [FromHeader] string password)
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

                //Get OrderItem By OrderItemId
                var OrderItem = await _unitOfwork.OrderItemRepository.GetOrderItemById(OrderItemId);
                //Check Order Is Found
                if (OrderItem != null)
                {
                    Log.Information("OrderItem Is  Existing");

                    //DeleteOrderItem And Save Changes In Database

                    await _unitOfwork.OrderItemRepository.DeleteOrderItem(OrderItem);


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

    }




}

