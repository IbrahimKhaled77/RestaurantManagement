using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs.AuthanticationDTO;
using RestaurantManagement.DTOs.EmployeeDTO;
using RestaurantManagement.Model.Entity;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;
using Restaurants_Repository.Helper;
using Restaurants_Service.IService;
using Serilog;


namespace Restaurants_Service.Service
{
    public class EmployeeService :IEmployeeService
    {
        private readonly IUnitOfwork _unitOfwork;
        public EmployeeService(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }

      

        public async Task<List<EmployeeCardDTO>> GetAllEmployees([FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork. EmployeeRepository.IsEmployeeLoggedIn(email, password);
                //Check isAdminLoggedIn Not Found
                if (!isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                // Get All of List Employees 
                var Employees = await _unitOfwork. EmployeeRepository.GetAllEmployees();

                //Check Is Admin 
                if (Employees == null)
                {
                    Log.Error($"Employees Not Found ");
                    throw new ArgumentNullException("Employees", "Not Found Employees");
                }
               
                var Result = from employeeDto in Employees
                             select new EmployeeCardDTO
                             {

                                 EmployeeId = employeeDto.EmployeeId,
                                 Name = employeeDto.Name,
                                 Email = employeeDto.Email,
                                 Position = employeeDto.Position,
                                 IsActive = employeeDto.IsActive,
                             };
                Log.Information("Employees are Reached");
                Log.Debug($"Debugging GetAllEmployees Has been Finised Successfully");
                return (Result.ToList());

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Employee Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception : {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public async Task<EmployeeCardDTO> GetEmployeeById(int employeeId, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                //Searches for the EmployeeId in the Employee table. The Employee must be logged in
                var isEmployeeLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password);

                //check if the isEmployeeLoggedIn  not equal null
                if (!isEmployeeLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                // Get Employee By EmployeeId
                var Employee = await _unitOfwork.EmployeeRepository.GetEmployeeById(employeeId);

                // Get Employee Is null
                if (Employee == null)
                {
                    Log.Error($"Employee Not Found ");
                    throw new ArgumentNullException("Employee", "Not Found Employee");
                }
                //Create Employee Card DTO
                EmployeeCardDTO employeeCardDTO = new EmployeeCardDTO()
                {
                   EmployeeId = Employee.EmployeeId,
                    Name = Employee.Name,
                    Email = Employee.Email,
                    Position = Employee.Position,
                    IsActive = Employee.IsActive,

            };
                Log.Information("Employee Is Reached");
                Log.Debug($"Debugging GetEmployeeById Has been Finised Successfully With Employee ID  = {Employee.EmployeeId}");
                return employeeCardDTO;


            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Employee Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception : {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }


        public async Task<string> AddEmployee(CreatEmployeeDTO employeeDto, [FromHeader] string email , [FromHeader] string password)
        {

            try
            {

                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");

                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");

                //Check Is Admin 
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }

                Log.Information("Create New Employee");

                //Create Employee 
                var Employee = new Employee()
                {
                 Name = employeeDto.Name,
                Email = employeeDto.Email,
                Password = employeeDto.Password,
                Position = employeeDto.Position,
                IsActive = true,
                IsLoggedIn = false,
                AccessKey = "",


            };

                // Add Employee 
                await _unitOfwork. EmployeeRepository.AddEmployee(Employee);

                Log.Information("Employee Is In Finised");
                Log.Debug($"Debugging AddEmployee Has been Finised Successfully With Employee ID  {Employee.EmployeeId} ");
                return "AddEmployee Has been Finised Successfully";
            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred in datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Employee Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

       

        public async Task<string> UpdateEmployee(UpdateEmployeeDTO employeeDto, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {
                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");

                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");

                //Check Is Admin 
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }
                //Get Employee by EmployeeId
                var employee = await _unitOfwork. EmployeeRepository.GetEmployeeById(employeeDto.EmployeeId);

                //Check employee is not Found
                if (employee == null)
                {
                    Log.Error($"Employee Not Found ");
                    throw new ArgumentNullException("Employee", "Not Found");
                }
                Log.Information("Employee Is  Existing");

                //Update Employee
                employee.Name = employeeDto.Name;
                employee.Email = employeeDto.Email;
                employee.Password = employeeDto.Password;
                employee.Position = employeeDto.Position;
                employee.IsActive = employeeDto.IsActive;

                //Update Employee And SaveChange In Database
                await _unitOfwork. EmployeeRepository.UpdateEmployee(employee);

                Log.Information("Employee Is Updated");
                Log.Debug($"Debugging UpdateEmployee Has been Finised Successfully With Employee ID  = {employee.EmployeeId}");

                return "UpdateEmployee Has been Finised Successfully";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Employee Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public async Task<string> DeleteEmployee(int employeeId, [FromHeader] string email, [FromHeader] string password)
        {

            try
            {

                //Searches for the EmployeeID in the Employee table. The Employee must be logged in
                var isAdminLoggedIn = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedIn(email, password, "Admin");

                //Check isAdminLoggedIn Not Found
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }
                //Searches for the EmployeeId in the Employee table. The Employee  Must be  Position Accountant
                var isAdmin = await _unitOfwork.EmployeeRepository.IsEmployeeLoggedAdminIn(email, password, "Admin");

                //Check Is Admin 
                if (!isAdmin)
                {

                    throw new Exception("You Don't have the required Permission");
                }
                //Get Employee By EmployeeId
                var employee = await _unitOfwork. EmployeeRepository.GetEmployeeById(employeeId);

                //Check is null
                if (employee == null)
                {
                    Log.Error($"Employee Not Found ");
                    throw new ArgumentNullException("Employee", "Not Found Employee");
                }
                Log.Information("Employee Is In Existing");

                //Delete And SaveChanges in Database
                await _unitOfwork. EmployeeRepository.DeleteEmployee(employee);

                Log.Information("Employee Is Deleted");
                Log.Debug($"Debugging DeleteEmployee Has been Finised Successfully With Employee ID  = {employee.EmployeeId}");
                return "DeleteEmployee Has been Finised Successfully";
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"Employee Not Found: {ex.Message}");
                throw new DbUpdateException($"datebase Error: {ex.Message}");

            }
            catch (DbUpdateException ex)
            {
                Log.Error($"An error occurred In datebase: {ex.Message}");
                throw new DbUpdateException($"date Error: {ex.Message}");

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred ,Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }
        }

        public async Task<string> LoginEmployee(AuthanticationDTOs AuthanticationDTOs)
        {
            try
            {
                Log.Information("Starting Login");
                //check if the Email equal null Or Empty
                if (string.IsNullOrEmpty(AuthanticationDTOs.Email))
                    throw new Exception("Email Is Required");

                //check if the Password equal null Or Empty
                if (string.IsNullOrEmpty(AuthanticationDTOs.Password))
                    throw new Exception("Password Is Required");

                // Get Employee 
                var Employee = await _unitOfwork.EmployeeRepository.LoginEmployee(AuthanticationDTOs.Email, AuthanticationDTOs.Password);

                //check if the Employee not equal null
                if (Employee != null)
                {
                    if (!Employee.IsLoggedIn)
                    {


                        //Create a new Token Employee
                        Helper.TokenEmployee(Employee);

                        //Update Employee And SaveChanges In Database
                        await _unitOfwork.EmployeeRepository.UpdateEmployee(Employee);

                        Log.Information("LoginEmployee Is In Finised");
                        Log.Debug($"Debugging LoginEmployee Has been Finised Successfully With finalToken  {Helper.TokenEmployee(Employee)} ");


                        return Helper.TokenEmployee(Employee);
                    }
                    //Is Employee  equal null 
                    else
                    {
                        //Logout
                        Employee.IsLoggedIn = false;

                        //Update Employee And SaveChanges In Database
                        await _unitOfwork.EmployeeRepository.UpdateEmployee(Employee);

                        Log.Information("Employee Is not Found");
                        Log.Debug($"Youre Session Has been Closed Please Login in Again");

                        return "Youre Session Has been Closed Please Login in Again";
                    }
                }
                else
                {
                    Log.Information("Either Email or Password is Incorrect");
                    Log.Debug($"Either Email or Password is Incorrect");
                    return "Either Email or Password is Incorrect";
                }


            }
            catch (Exception ex)
            {

                Log.Error($"An error occurred Exception: {ex.Message}");
                throw new Exception($"Exception: {ex.Message}");

            }




        }

    }
}
