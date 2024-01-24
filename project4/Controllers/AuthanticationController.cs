using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.AuthanticationDTO;

using RestaurantManagement_Repository.UnitOfWorkPattern.IUnitOfWork;

namespace RestaurantManagement.Controllers
{
    public class AuthanticationController :ControllerBase
    {
        private readonly IUnitOfwork _IUnitOfwork;

        public AuthanticationController(IUnitOfwork UnitOfwork)
        {
            _IUnitOfwork = UnitOfwork;
        }



        #region HttpPut Logout
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/Logout
        ///     {        
        ///       "Id": "Enter the  user  Id (Required)",
        ///     }
        /// </remarks>
        /// <response code="201">Returns  LoginCustomer  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Remove a Token customer to the database.
        /// </summary>
        /// <param name="UserId">The User Id  to Logout (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("Logout")]
        public async Task<IActionResult> Logout(int UserId)
        {
            try
            {
                return StatusCode(201, await _IUnitOfwork._IAuthanticationRepository.Logout(UserId));

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

        #region HttpPost ResetPassword
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/ResetPassword
        ///     {        
        ///       "Email": "Enter the  user  Email ",
        ///       "NewPassword": "Enter the  user  NewPassword ",
        ///     }
        /// </remarks>
        /// <response code="201">Returns  ResetPassword  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new Password  to customer by Email .
        /// </summary>
        /// <param Email="Email">The Email of the  User to ResetPassword (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO ResetPasswordDTO)
        {
            try
            {
                return StatusCode(201, await _IUnitOfwork._IAuthanticationRepository.ResetPassword(ResetPasswordDTO));

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

