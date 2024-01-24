using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.AuthanticationDTO;
using RestaurantManagement_Repository.DTOs.EmployeeDTO;
using RestaurantManagement_Repository.UnitOfWorkPattern.IUnitOfWork;

namespace RestaurantManagement.Controllers
{
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfwork _IUnitOfwork;

        public EmployeeController(IUnitOfwork UnitOfwork)
        {
            _IUnitOfwork = UnitOfwork;
        }



        #region HttpPost LoginEmployee
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/LoginEmployee
        ///     {        
        ///        "EmailEmployee": "Enter Your EmailEmployee Here (Required)",
        ///        "passwordEmployee": "Enter Your passwordEmployee Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  LoginEmployee  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new Token Employee to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee to Login (Required).</param>
        /// <param name="Password">The Password of the  Employee to Login (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("LoginEmployee")]
        public async Task<IActionResult> LoginEmployee(AuthanticationDTOs AuthanticationDTOs)
        {
            try
            {
                return StatusCode(201, await _IUnitOfwork._IEmployeeRepository.LoginEmployee(AuthanticationDTOs));

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

        #region HttpGet GetAllEmployees & GetEmployeeById
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/GetAllEmployees
        ///     {        
        ///        "EmailEmployee": "Enter Your EmailEmployee Here (Required)",
        ///        "passwordEmployee": "Enter Your passwordEmployee Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get All Employees Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Employees present on the application.
        /// </summary>
        /// <param name="Email">The Email of the  Employee to Get All Employees (Required).</param>
        /// <param name="Password">The Password of the  Employee to Get All Employees (Required).</param>
        /// <returns>List of Employees </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllEmployees( [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _IUnitOfwork._IEmployeeRepository.GetAllEmployees(Email, Password));

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
        ///     Get api/GetEmployeeById
        ///     {        
        ///       "EmployeeId": "Enter Your Employee ID Here (Required)",
        ///        "EmailEmployee": "Enter Your EmailEmployee Here (Required)",
        ///        "passwordEmployee": "Enter Your passwordEmployee Here (Required)",
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get  Employee by EmployeeID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a Employee by ID from the application
        /// </summary>
        /// <param name="EmployeeId">The ID of the Employee to retrieve (Required).</param>
        /// <param name="Email">The Email of the  Employee to Get Employee By Id (Required).</param>
        /// <param name="Password">The Password of the  Employee to  Get Employee By Id (Required).</param>
        /// <returns>The Employee information. </returns>
        [HttpGet]
        [Route("[action]/{EmployeeId}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int EmployeeId,[FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _IUnitOfwork._IEmployeeRepository.GetEmployeeById(EmployeeId, Email, Password));

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

        #region HttpPost AddEmployee
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/AddEmployee
        ///     {        
        ///        "NameEmployee": "Enter Your NameEmployee Here (Required)",
        ///        "EmailEmployee": "Enter Your EmailEmployee Here (Required)",
        ///        "passwordEmployee": "Enter Your passwordEmployee Here (Required)",
        ///        "phoneNumberEmployee": "Enter Your phoneNumberEmployee Here (Required)",
        ///        "Position":"Enter one of these options as Waiter, Chef, or Admin (Required) "
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Add Employee  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new Employee to the database.
        /// </summary>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddEmployee([FromBody]CreatEmployeeDTO creatEmployee)
        {
            try
            {
                return StatusCode(201, await _IUnitOfwork._IEmployeeRepository.AddEmployee(creatEmployee));

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

        #region HttpPut UpdateEmployee
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put api/UpdateEmployee
        ///     {     
        ///        "EmployeeId": "Enter your Employee ID whose information you want to update",
        ///        "NameEmployee": "Enter Your NameEmployee Here (Required)",
        ///        "EmailEmployee": "Enter Your EmailEmployee Here (Required)",
        ///        "passwordEmployee": "Enter Your passwordEmployee Here (Required)",
        ///        "phoneNumberEmployee": "Enter Your phoneNumberEmployee Here (Required)",
        ///         "Position":"Enter one of these options as Waiter, Chef, or Admin (Required) "
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Update Employee Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Update a  Employee to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Employee to Update Employee (Required).</param>
        /// <param name="Password">The Password of the  Employee to  Update Employee (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDTO UpdateEmployeeDTO,[FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _IUnitOfwork._IEmployeeRepository.UpdateEmployee(UpdateEmployeeDTO, Email, Password));

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

        #region HttpDelete DeleteEmployee
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete api/DeleteEmployee
        ///     {     
        ///        "EmployeeId": "Enter your Employee ID whose information you want to Delete",
        ///        "EmailEmployee": "Enter Your EmailEmployee Here (Required)",
        ///        "passwordEmployee": "Enter Your passwordEmployee Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Delete Employee Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Delete a  Employee from the database.
        /// </summary>
        /// <param name="EmployeeId">The ID of the Employee to Delete (Required).</param>
        /// <param name="Email">The Email of the  Employee to Delete Employee (Required).</param>
        /// <param name="Password">The Password of the  Employee to  Delete Employee (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int EmployeeId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201,await _IUnitOfwork._IEmployeeRepository.DeleteEmployee(EmployeeId, Email, Password));

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
