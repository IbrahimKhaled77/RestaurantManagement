using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.OrderDTO;
using Restaurants_Service.IService;

namespace RestaurantManagement.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
;
        }


        #region HttpGet GetAllOrders & GetOrderById
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get api/GetAllOrders
        ///     {        
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get All Orders Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Orders present on the application.
        /// </summary>
        /// <param name="Email">The Email of the  User to Get All Orders (Required).</param>
        /// <param name="Password">The Password of the  User to Get All Orders (Required).</param>
        /// <returns>List of Orders </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrders([FromHeader] string Email, [FromHeader] string Password)
        {

            try
            {
                return StatusCode(201,await _orderService.GetAllOrders(Email, Password));

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
        ///     Get api/GetOrderById
        ///     {        
        ///       "OrderId": "Enter Your Order ID Here (Required)",  
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get  Order by OrderID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a Order by ID from the application
        /// </summary>
        /// <param name="OrderId">The ID of the Order to retrieve (Required).</param>
        /// <param name="Email">The Email of the  User to Get All Order By Id (Required).</param>
        /// <param name="Password">The Password of the  User to Get All Orders By Id (Required).</param>
        /// <returns>The Order information. </returns>
        [HttpGet]
        [Route("[action]/{OrderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute]  int OrderId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201,await _orderService.GetOrderById(OrderId, Email, Password));

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

        #region HttpPost AddOrder
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/AddOrder
        ///     {        
        ///        "Email": "Enter Your Email Employee (Admin Or Customer)  Here (Required)",
        ///        "password": "Enter Your password  Employee (Admin Or Customer) Here (Required)",
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
        /// <param name="Password">The Password of the  Employee (Admin Or Customer)  to Add Order(Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> AddOrder(CreatOrderDTO order, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201,await _orderService.AddOrder(order, Email, Password));

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

        #region HttpPut UpdateOrder
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put api/UpdateOrder
        ///     {     
        ///        "Email": "Enter Your Email Employee (Admin Or Customer) Here (Required)",
        ///        "password": "Enter Your password  Employee (Admin Or Customer) Here (Required)",
        ///        "OrderId": "Enter your Order ID whose information you want to update",
        ///        "TableId": "Enter your table ID here to place your order(Required)",
        ///        "TotalPrice": "Enter Your TotalPrice to Order Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Update Order Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Update a  Order(Item)  to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee (Admin Or Customer) to Update Order (Required).</param>
        /// <param name="Password">The Password of the  Employee (Admin Or Customer)  to Update Order (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO order,[FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201,await _orderService.UpdateOrder(order, Email, Password));

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

        #region HttpDelete DeleteOrder
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete api/DeleteOrder
        ///     {    
        ///        "Email": "Enter Your Email Employee (Admin Or Customer) Here (Required)",
        ///        "password": "Enter Your password  Employee (Admin Or Customer) Here (Required)",
        ///        "OrderId": "Enter your Order ID whose information you want to Delete",
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
        /// <param name="Email">The Email of the  Employee(Admin) to Delete Order (Required).</param>
        /// <param name="Password">The Password of the  Employee(Admin)  to Delete Order (Required).</param>
        /// <param name="OrderId">The ID of the Order to Delete (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{OrderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute]int OrderId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201,await _orderService.DeleteOrder(OrderId, Email, Password));

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

