using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.CustomerDTO;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;

namespace RestaurantManagement.Controllers
{
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        #region HttpGet

        /// <response code="201">Returns  Get All Customers Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the customers present on the application.
        /// </summary>
        /// <returns>List of Customers </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                
                return StatusCode(201, await _customerRepository.GetAllCustomers());

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
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get  Customer by CustomerID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a customer by ID from the application
        /// </summary>
        /// <param name="customerId">The ID of the customer to retrieve (Required).</param>
        /// <returns>The customer information. </returns>
        [HttpGet]
        [Route("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int CustomerId)
        {
            try
            {
                return StatusCode(201, await _customerRepository.GetCustomerById(CustomerId));

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

        #region HttpPost
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
                return StatusCode(201, await _customerRepository.AddCustomer(createCustomerDTO));

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

        #region HttpPut
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
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            try
            {
                return StatusCode(201, await _customerRepository.UpdateCustomer(updateCustomerDTO));

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

        #region HttpDelete
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
        /// <param name="customerId">The ID of the customer to Delete (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{CustomerId}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int CustomerId)
        {
            try
            {
                return StatusCode(201, await _customerRepository.DeleteCustomer(CustomerId));

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
