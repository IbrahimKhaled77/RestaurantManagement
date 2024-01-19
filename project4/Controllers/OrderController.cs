using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _OrderRepository;

        public OrderController(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }


        #region HttpGet
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrders()
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
        public async Task<IActionResult> GetOrderById()
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
        public async Task<IActionResult> AddOrder()
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
        public async Task<IActionResult> UpdateOrder()
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
        public async Task<IActionResult> DeleteOrder()
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

