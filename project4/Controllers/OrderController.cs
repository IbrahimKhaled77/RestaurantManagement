using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.OrderDTO;
using RestaurantManagement_Repository.IRepository;


namespace RestaurantManagement.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _OrderRepository;

        public OrderController(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }


        #region HttpGet GetAllOrders & GetOrderById
        /// <response code="201">Returns  Get All Orders Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Orders present on the application.
        /// </summary>
        /// <returns>List of Orders </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrders([FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                return StatusCode(201,await _OrderRepository.GetAllOrders(email, password));

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
        /// <returns>The Order information. </returns>
        [HttpGet]
        [Route("[action]/{OrderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute]  int OrderId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201,await _OrderRepository.GetOrderById(OrderId, email, password));

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
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> AddOrder(CreatOrderDTO order, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201,await _OrderRepository.AddOrder(order, email, password));

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
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO order,[FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201,await _OrderRepository.UpdateOrder(order, email, password));

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
        /// <param name="OrderId">The ID of the Order to Delete (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{OrderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute]int OrderId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201,await _OrderRepository.DeleteOrder(OrderId, email, password));

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

