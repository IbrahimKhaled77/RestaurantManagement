using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.TableDTO;
using RestaurantManagement_Repository.UnitOfWorkPattern.IUnitOfWork;

namespace RestaurantManagement.Controllers
{
    public class TableController : ControllerBase
    {
        private readonly IUnitOfwork _IUnitOfwork;
        
        public TableController(IUnitOfwork UnitOfwork)
        {
            _IUnitOfwork = UnitOfwork;
        }


        #region HttpGet GetAllTables & GetTableById
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
        /// <response code="201">Returns  Get All Table Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Table present on the application.
        /// </summary>
        /// <param name="Email">The Email of the  User to Get All Tables (Required).</param>
        /// <param name="Password">The Password of the  User to Get All Tables (Required).</param>
        /// <returns>List of Table </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllTables([FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {

                return StatusCode(201,await _IUnitOfwork._ITableRepository.GetAllTables(Email, Password));

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
        ///     Get api/GetTableById
        ///     {        
        ///       "TableId": "Enter Your Table ID Here (Required)",
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get  Table by TableID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a Table by ID from the application
        /// </summary>
        /// <param name="TableId">The ID of the Table to retrieve (Required).</param>
        /// <param name="Email">The Email of the  User to Get All Tables (Required).</param>
        /// <param name="Password">The Password of the  User to Get All Tables (Required).</param>
        /// <returns>The Table information. </returns>
        [HttpGet]
        [Route("[action]/{TableId}")]
        public async Task<IActionResult> GetTableById([FromRoute] int TableId,[FromHeader] string Email, [FromHeader] string Password)
        {

            try
            {
                return StatusCode(201,await _IUnitOfwork._ITableRepository.GetTableById(TableId, Email, Password));

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

        #region HttpPost AddTable
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/AddTable
        ///     {        
        ///        "Email": "Enter Your Email  Employee(Admin) Here (Required)",
        ///      "password": "Enter Your password Employee(Admin) Here (Required)",
        ///        "TableNumber": "Enter Your TableNumber Here (Required)",
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Add Table  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new Table  to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee(Admin) to Add Table (Required).</param>
        /// <param name="Password">The Password of the  Employee(Admin)  to Add Table (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddTable([FromBody] CreatTableDTO Table, [FromHeader] string Email, [FromHeader] string Password)
        {
          
           
            try
            {
               
                return StatusCode(201,await _IUnitOfwork._ITableRepository.AddTables(Table, Email, Password));

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

        #region HttpPut UpdateTable
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put api/UpdateTable
        ///     {     
        ///        "Email": "Enter Your Email  Employee(Admin) Here (Required)",
        ///       "password": "Enter Your password Employee(Admin) Here (Required)",
        ///        "TableId": "Enter your Table ID whose information you want to update",
        ///        "NameTable": "Enter Your NameTable(Item) Here (Required)",
        ///        "DescriptionTable": "Enter Your DescriptionTable(Item) Here (Required)",
        ///        "PriceTable": "Enter Your PriceTable(Item) Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Update Table Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Update a  Table(Item)  to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee(Admin) to Update Table (Required).</param>
        /// <param name="Password">The Password of the  Employee(Admin)  to Update Table (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDto TableDto, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201,await _IUnitOfwork._ITableRepository.UpdateTable(TableDto, Email, Password));

            }
            catch (ArgumentNullException ex)
            {

                return StatusCode(404, ex.Message);

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }
        #endregion

        #region HttpDelete DeleteTable
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete api/DeleteTable
        ///     {     
        ///        "Email": "Enter Your Email  Employee(Admin) Here (Required)",
        ///       "password": "Enter Your password Employee(Admin) Here (Required)",
        ///        "TableId": "Enter your Table ID whose information you want to Delete",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Delete Table Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Delete a  Table from the database.
        /// </summary>
        /// <param name="TableId">The ID of the Table to Delete (Required).</param>
        /// <param name="Email">The Email of the  Employee(Admin) to Update Table (Required).</param>
        /// <param name="Password">The Password of the  Employee(Admin)  to Update Table (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{TableId}")]
        public async Task<IActionResult> DeleteTable([FromRoute] int TableId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {

                return StatusCode(201,await _IUnitOfwork._ITableRepository.DeleteTable(TableId, Email, Password));

            }
            catch (ArgumentNullException ex)
            {
                
                return StatusCode(404, ex.Message);

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }
        #endregion
    }
}

