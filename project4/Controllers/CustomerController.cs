using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.AuthanticationDTO;
using RestaurantManagement.DTOs.CustomerDTO;
using Restaurants_Service.IService;

namespace RestaurantManagement.Controllers
{
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService CustomerService)
        {
            _customerService = CustomerService;
        }



        #region HttpPost LoginCustomer
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/LoginCustomer
        ///     {        
        ///        "EmailCustomer": "Enter Your EmailCustomer Here (Required)",
        ///        "passwordCustomer": "Enter Your passwordCustomer Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  LoginCustomer  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new Token customer to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Customer to Login (Required).</param>
        /// <param name="Password">The Password of the  Customer to Login (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("LoginCustomer")]
        public async Task<IActionResult> LoginCustomer(AuthanticationDTOs AuthanticationDTOs)
        {
            try
            {
                return StatusCode(201, await _customerService.LoginCustomer(AuthanticationDTOs));

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


        #region HttpGet GetAllCustomers & GetCustomerById

        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/GetAllCustomers
        ///     {        
        ///        "EmailCustomer": "Enter Your EmailCustomer Here (Required)",
        ///        "passwordCustomer": "Enter Your passwordCustomer Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get All Customers Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the customers present on the application.
        /// </summary>
        /// <param name="Email">The Email of the  Customer to Get All Customers (Required).</param>
        /// <param name="Password">The Password of the  Customer to Get All Customers (Required).</param> 
        /// <returns>List of Customers </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllCustomers([FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                
                return StatusCode(201, await _customerService.GetAllCustomers(Email, Password));

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
        ///     Get api/GetCustomerById
        ///     {        
        ///       "CustomerId": "Enter Your Customer ID Here (Required)", 
        ///        "EmailCustomer": "Enter Your EmailCustomer Here (Required)",
        ///        "passwordCustomer": "Enter Your passwordCustomer Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get  Customer by CustomerID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a customer by ID from the application
        /// </summary>
        /// <param name="CustomerId">The ID of the customer to retrieve (Required).</param>
        /// <param name="Email">The Email of the  Customer to Get Customer By Id (Required).</param>
        /// <param name="Password">The Password of the  Customer to Get Customer By Id (Required).</param> 
        /// <returns>The customer information. </returns>
        [HttpGet]
        [Route("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int CustomerId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _customerService.GetCustomerById(CustomerId, Email, Password));

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

        #region HttpPost AddCustomer
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/AddCustomer
        ///     {        
        ///        "NameCustomer": "Enter Your NameCustomer Here (Required)",
        ///        "EmailCustomer": "Enter Your EmailCustomer Here (Required)",
        ///        "passwordCustomer": "Enter Your passwordCustomer Here (Required)",
        ///        "phoneNumberCustomer": "Enter Your phoneNumberCustomer Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Add Customer  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddCustomer([FromBody]CreateCustomerDTO createCustomerDTO)
        {
            try
            {
                return StatusCode(201, await _customerService.AddCustomer(createCustomerDTO));

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

        #region HttpPut UpdateCustomer
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put api/UpdateCustomer
        ///     {     
        ///        "CustomerId": "Enter your customer ID whose information you want to update",
        ///        "NameCustomer": "Enter Your NameCustomer Here (Required)",
        ///        "EmailCustomer": "Enter Your EmailCustomer Here (Required)",
        ///        "passwordCustomer": "Enter Your passwordCustomer Here (Required)",
        ///        "phoneNumberCustomer": "Enter Your phoneNumberCustomer Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Update Customer Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Update a  customer to the database.
        /// </summary>
        /// <param name="Email">The Email of the  Customer to Update Customer (Required).</param>
        /// <param name="Password">The Password of the  Customer to Update Customer (Required).</param> 
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO updateCustomerDTO, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _customerService.UpdateCustomer(updateCustomerDTO, Email, Password));

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

        #region HttpDelete DeleteCustomer
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete api/DeleteCustomer
        ///     {     
        ///        "CustomerId": "Enter your customer ID whose information you want to Delete",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Delete Customer Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Delete a  customer from the database.
        /// </summary>
        /// <param name="CustomerId">The ID of the user to Delete (Required).</param>
        /// <param name="Email">The Email of the  user to Delete Customer (Required).</param>
        /// <param name="Password">The Password of the  Customer to Delete Customer (Required).</param> 
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{CustomerId}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int CustomerId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _customerService.DeleteCustomer(CustomerId, Email, Password));

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
