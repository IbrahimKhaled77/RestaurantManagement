using Microsoft.AspNetCore.Mvc;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement.Controllers
{
    public class MenuController : Controller
    {

        private readonly IMenuRepository _MenuRepository;

        public MenuController(IMenuRepository MenuRepository)
        {
            _MenuRepository = MenuRepository;
        }


        #region HttpGet
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllMenus()
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
        public async Task<IActionResult> GetMenuById()
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
        public async Task<IActionResult> AddMenu()
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
        public async Task<IActionResult> UpdateMenu()
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
        public async Task<IActionResult> DeleteMenu()
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

