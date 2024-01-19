using Microsoft.AspNetCore.Mvc;

using RestaurantManagement_Repository.IRepository;

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
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllCustomers()
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
        public async Task<IActionResult> GetCustomerById()
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
        public async Task<IActionResult> AddCustomer()
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
        public async Task<IActionResult> UpdateCustomer()
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
        public async Task<IActionResult> DeleteCustomer()
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
