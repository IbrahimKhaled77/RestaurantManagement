using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.DTOs.MenuDTO;
using RestaurantManagement_Repository.IRepository;


namespace RestaurantManagement.Controllers
{
    public class MenuController : ControllerBase
    {

        private readonly IMenuRepository _MenuRepository;

        public MenuController(IMenuRepository MenuRepository)
        {
            _MenuRepository = MenuRepository;
        }


        #region HttpGet GetAllMenus & GetMenuById
        /// <response code="201">Returns  Get All Menu Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Menu present on the application.
        /// </summary>
        /// <returns>List of Menu </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllMenus([FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201, await _MenuRepository.GetAllMenus(email, password));

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
        ///     Get api/GetMenuById
        ///     {        
        ///       "MenuId": "Enter Your Menu ID Here (Required)",  
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Get  Menu by MenuID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a Menu by ID from the application
        /// </summary>
        /// <param name="MenuId">The ID of the Menu to retrieve (Required).</param>
        /// <returns>The Menu information. </returns>
        [HttpGet]
        [Route("[action]/{MenuId}")]

        public async Task<IActionResult> GetMenuById([FromRoute]int MenuId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201, await _MenuRepository.GetMenuById(MenuId, email, password));

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

        #region HttpPost AddMenu
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post api/AddMenu
        ///     {        
        ///        "NameMenu": "Enter Your NameMenu(Item) Here (Required)",
        ///        "DescriptionMenu": "Enter Your DescriptionMenu(Item) Here (Required)",
        ///        "PriceMenu": "Enter Your PriceMenu(Item) Here (Required)",
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Add Menu  Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Adds a new Menu (Item) to the database.
        /// </summary>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMenu([FromBody]CreatMenuDTO menu, [FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {
                return StatusCode(201, await _MenuRepository.AddMenus(menu, email, password));

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

        #region HttpPut UpdateMenu
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put api/UpdateMenu
        ///     {     
        ///        "MenuId": "Enter your Menu ID whose information you want to update",
        ///        "NameMenu": "Enter Your NameMenu(Item) Here (Required)",
        ///        "DescriptionMenu": "Enter Your DescriptionMenu(Item) Here (Required)",
        ///        "PriceMenu": "Enter Your PriceMenu(Item) Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Update Menu Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Update a  Menu(Item)  to the database.
        /// </summary>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateMenu([FromBody] UpdateMenuDTO MenuDTo, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201, await _MenuRepository.UpdateMenu(MenuDTo, email, password));

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

        #region HttpDelete DeleteMenu
        /// <remarks>
        /// Sample request:
        /// 
        ///     Delete api/DeleteMenu
        ///     {     
        ///        "MenuId": "Enter your Menu ID whose information you want to Delete",
        ///      
        ///     }
        /// </remarks>
        /// <response code="201">Returns  Delete Menu Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Delete a  Menu from the database.
        /// </summary>
        /// <param name="MenuId">The ID of the Menu to Delete (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{MenuId}")]
        public async Task<IActionResult> DeleteMenu([FromRoute]int MenuId, [FromHeader] string email, [FromHeader] string password)
        {
            try
            {
                return StatusCode(201, await _MenuRepository.DeleteMenu(MenuId, email, password));

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

