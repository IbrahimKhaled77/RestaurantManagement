using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.OrderItemDTO;
using Restaurants_Service.IService;

namespace RestaurantManagement.Controllers
{
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService OrderItemService)
        {
            _orderItemService = OrderItemService;
        }


        #region HttpGet GetAllOrderItems & GetOrderItemById
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get api/GetAllOrderItems
        ///     {        
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get All Orders item Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Orders item present on the application.
        /// </summary>
        /// <param name="Email">The Email of the  User to Get All Orders  item(Required).</param>
        /// <param name="Password">The Password of the  User to Get All Orders item (Required).</param>
        /// <returns>List of Orders </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrderItems([FromHeader] string Email, [FromHeader] string Password)
        {

            try
            {
                return StatusCode(201, await _orderItemService.GetAllOrderItems(Email, Password));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);

            }
            catch (ArgumentNullException ex)
            {

                return StatusCode(404, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        /// <remarks>
        /// Sample request:
        /// 
        ///     Get api/GetOrderItemById
        ///     {        
        ///       "OrderItemId": "Enter Your OrderItem ID Here (Required)",  
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get  Order item by OrderID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a Order by ID from the application
        /// </summary>
        /// <param name="OrderItemId">The ID of the Order item to retrieve (Required).</param>
        /// <param name="Email">The Email of the  User to Get All Order item  By Id (Required).</param>
        /// <param name="Password">The Password of the  User to Get All Orders item By Id (Required).</param>
        /// <returns>The Order item information. </returns>
        [HttpGet]
        [Route("[action]/{OrderItemId}")]
        public async Task<IActionResult> GetOrderItemById([FromRoute] int OrderItemId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _orderItemService.GetOrderItemById(OrderItemId, Email, Password));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);

            }
            catch (ArgumentNullException ex)
            {

                return StatusCode(404, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        #endregion

        #region HttpPost AddOrderItem
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/AddOrder
        ///     {        
        ///        "Email": "Enter Your Email Employee ((Admin Or Customer))  Here (Required)",
        ///        "password": "Enter Your password  Employee ((Admin Or Customer)) Here (Required)",
        ///        "CustomerId": "Enter your customer ID here if you want to order (Required)",
        ///        "TableId": "Enter your table ID here to place your order(Required)",
        ///        "TotalPrice": "Enter Your TotalPrice to Order Here (Required)",
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Add Order  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new Order  to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee (Admin Or Customer) to Add Order (Required).</param>
        /// <param name="Password">The Password of the  Employee ((Admin Or Customer))  to Add Order(Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> AddOrderItem(CreateOrderItemDTO OrderItemDTO, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _orderItemService.AddOrderItem(OrderItemDTO, Email, Password));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);

            }
            catch (ArgumentNullException ex)
            {

                return StatusCode(404, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        #endregion

        #region HttpPut UpdateOrderItem
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put api/UpdateOrderItem
        ///     {     
        ///        "Email": "Enter Your Email Employee ((Admin Or Customer)) Here (Required)",
        ///        "password": "Enter Your password  Employee ((Admin Or Customer)) Here (Required)",
        ///        "OrderItemId": "Enter your OrderItem ID whose information you want to update",
        ///        "OrderId":"Enter your Order ID here to order item  (Required)"
        ///        "MenuId": "Enter your Menu ID here to order item (Required)",
        ///        "Quantity": "Enter Your Quantity to Item Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Update Order item Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Update a  Order(Item)  to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee ((Admin Or Customer)) to Update Order item (Required).</param>
        /// <param name="Password">The Password of the  Employee ((Admin Or Customer))  to Update Order item (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderIterm orderItemDTO, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _orderItemService.UpdateOrderItem(orderItemDTO, Email, Password));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);

            }
            catch (ArgumentNullException ex)
            {

                return StatusCode(404, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        #endregion

        #region HttpDelete DeleteOrderItem
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete api/DeleteOrder
        ///     {    
        ///        "Email": "Enter Your Email Employee (Admin) Here (Required)",
        ///        "password": "Enter Your password  Employee (Admin) Here (Required)",
        ///        "OrderIdItem": "Enter your OrderIdItem  ID whose information you want to Delete",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Delete Order Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Delete a  Order from the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee (Admin) to Delete Order (Required).</param>
        /// <param name="Password">The Password of the  Employee (Admin)  to Delete Order (Required).</param>
        /// <param name="OrderItemId">The ID of the Order to Delete (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{OrderItemId}")]
        public async Task<IActionResult> DeleteOrderItem([FromRoute]  int OrderItemId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _orderItemService.DeleteOrderItem(OrderItemId, Email, Password));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);

            }
            catch (ArgumentNullException ex)
            {

                return StatusCode(404, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }









        #endregion


    }
}
