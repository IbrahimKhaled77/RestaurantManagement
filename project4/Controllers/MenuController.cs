﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.MenuDTO;
using Restaurants_Service.IService;

namespace RestaurantManagement.Controllers
{
    public class MenuController : ControllerBase
    {

        private readonly IMenuService _IMenuService;

        public MenuController(IMenuService menuService)
        {
            _IMenuService = menuService;
        }


        #region HttpGet GetAllMenus & GetMenuById
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get api/GetAllMenus
        ///     {        
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",
        ///      
        ///     }
        /// </remarks>
        /// <response code="200">Returns  Get All Menu Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>    
        ///<summary>
        /// I will retrieve all the Menu present on the application.
        /// </summary>
        /// <param name="Email">The Email of the  User to Get All Menus (Required).</param>
        /// <param name="Password">The Password of the  User to Get All Menus (Required).</param>
        /// <returns>List of Menu </returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllMenus([FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(200, await _IMenuService.GetAllMenus(Email, Password));

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
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",
        ///     }
        /// </remarks>
        /// <response code="200">Returns  Get  Menu by MenuID Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If the error was occured  (Internal Server Error OR Database)</response>   
        /// <response code="400">If an exception occurs (Exception)</response>       
        ///<summary>
        /// Retrieves a Menu by ID from the application
        /// </summary>
        /// <param name="MenuId">The ID of the Menu to retrieve (Required).</param>
        /// <param name="Email">The Email of the  User to Get All Menus (Required).</param>
        /// <param name="Password">The Password of the  User to Get All Menus (Required).</param>
        /// <returns>The Menu information. </returns>
        [HttpGet]
        [Route("[action]/{MenuId}")]

        public async Task<IActionResult> GetMenuById([FromRoute]int MenuId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(200, await _IMenuService.GetMenuById(MenuId, Email, Password));

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
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",
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
        /// <param name="Email">The Email of the  Employee(Admin) to Add Menu (Required).</param>
        /// <param name="Password">The Password of the  Employee(Admin) to Add Menu (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMenu([FromBody] CreatMenuDTO menu, [FromHeader] string Email, [FromHeader] string Password)
        {
            
            try
            {
                return StatusCode(201, await _IMenuService.AddMenus(menu, Email, Password));

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
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",     
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
        /// <param name="Email">The Email of the  Employee(Admin) to Update Menu (Required).</param>
        /// <param name="Password">The Password of the  Employee(Admin) to Update Menu (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateMenu([FromBody] UpdateMenuDTO MenuDTo, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(201, await _IMenuService.UpdateMenu(MenuDTo, Email, Password));

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
        ///        "Email": "Enter Your Email Here (Required)",
        ///        "password": "Enter Your password Here (Required)",   
        ///        "MenuId": "Enter your Menu ID whose information you want to Delete",
        ///      
        ///     }
        /// </remarks>
        /// <response code="200">Returns  Delete Menu Successfully</response>
        /// <response code="404">If the error was occured  (Not Found)</response>
        /// <response code="500">If an internal server error or database error occurs (Internal Server Error OR Database)</response>   
        /// <response code="400">If the error was occured  (Exception)</response>       
        ///<summary>
        /// Delete a  Menu from the database.
        /// </summary>
        /// <param name="MenuId">The ID of the Menu to Delete (Required).</param>
        /// <param name="Email">The Email of the Employee(Admin) to Delete Menu (Required).</param>
        /// <param name="Password">The Password of the  Employee(Admin) to Delete Menu (Required).</param>
        /// <returns>A message indicating the success of the operation </returns>
        [HttpDelete]
        [Route("[action]/{MenuId}")]
        public async Task<IActionResult> DeleteMenu([FromRoute]int MenuId, [FromHeader] string Email, [FromHeader] string Password)
        {
            try
            {
                return StatusCode(200, await _IMenuService.DeleteMenu(MenuId, Email, Password));

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

