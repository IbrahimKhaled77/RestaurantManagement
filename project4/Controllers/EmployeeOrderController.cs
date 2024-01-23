using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.EmployeeDTO;
using RestaurantManagement_Repository.DTOs.EmployeeOrderCardDTO;
using RestaurantManagement_Repository.Implementation;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement.Controllers
{
    public class EmployeeOrderController : ControllerBase
    {
        private readonly IEmployeeOrderRepository _EmployeeOrderRepository;

        public EmployeeOrderController(IEmployeeOrderRepository EmployeeRepository)
        {
            _EmployeeOrderRepository = EmployeeRepository;
        }

        #region HttpPost
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/AddEmployee
        ///     {        
        ///        "NameEmployee": "Enter Your NameEmployee Here (Required)",
        ///        "EmailEmployee": "Enter Your EmailEmployee Here (Required)",
        ///        "passwordEmployee": "Enter Your passwordEmployee Here (Required)",
        ///        "phoneNumberEmployee": "Enter Your phoneNumberEmployee Here (Required)",
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
        public async Task<IActionResult> AddEmployeeOrder([FromBody] EmployeeOrderCreatDTOs employeeOrderCardDTOs)
        {
            try
            {
                return StatusCode(201, await _EmployeeOrderRepository.AddEmployeeOrder(employeeOrderCardDTOs));

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
