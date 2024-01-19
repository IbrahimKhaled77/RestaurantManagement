using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }


        #region HttpGet
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllEmployees()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEmployeeById()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region HttpPost
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddEmployee()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region HttpPut
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEmployee()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region HttpDelete
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteEmployee()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
