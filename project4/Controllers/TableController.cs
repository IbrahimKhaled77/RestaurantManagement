using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.TableDTO;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement.Controllers
{
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _TableRepository;

        public TableController(ITableRepository TableRepository)
        {
            _TableRepository = TableRepository;
        }


        #region HttpGet GetAllTables & GetTableById
        /// <response code="201">Returns  Get All Table Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Table present on the application.
        /// </summary>
        /// <returns>List of Table </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllTables([FromHeader] string email, [FromHeader] string password)
        {
            try
            {

                return StatusCode(201,await _TableRepository.GetAllTables(email, password));

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
        /// <returns>The Table information. </returns>
        [HttpGet]
        [Route("[action]/{TableId}")]
        public async Task<IActionResult> GetTableById([FromRoute] int TableId,[FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                return StatusCode(201,await _TableRepository.GetTableById(TableId, email,password));

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
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddTable([FromBody] CreatTableDTO table, [FromHeader] string email, [FromHeader] string password)
        {
          
           
            try
            {
               
                return StatusCode(201,await _TableRepository.AddTables(table, email, password));

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
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDto TableDto, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201,await _TableRepository.UpdateTable(TableDto, email, password));

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
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{TableId}")]
        public async Task<IActionResult> DeleteTable([FromRoute] int TableId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {

                return StatusCode(201,await _TableRepository.DeleteTable(TableId, email, password));

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

