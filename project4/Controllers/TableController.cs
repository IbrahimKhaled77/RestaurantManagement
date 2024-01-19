using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableRepository _TableRepository;

        public TableController(ITableRepository TableRepository)
        {
            _TableRepository = TableRepository;
        }


        #region HttpGet
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllTables()
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
        public async Task<IActionResult> GetTableById()
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
        public async Task<IActionResult> AddTable()
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
        public async Task<IActionResult> UpdateTable()
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
        public async Task<IActionResult> DeleteTable()
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

