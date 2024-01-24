

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.AuthanticationDTO;
using RestaurantManagement_Repository.DTOs.EmployeeDTO;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantManagement_Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantManagementContext _context;

        public EmployeeRepository(RestaurantManagementContext context)
        {
            _context = context;
        }
        public async Task<string> AddEmployee(CreatEmployeeDTO employeeDto)
        {
          
            try
            {
                Log.Information("Create New Employee");
                var Employee= new Employee();
                Employee.Name = employeeDto.Name;
                Employee.Email = employeeDto.Email;
                Employee.Password = employeeDto.Password;
                Employee.Position = employeeDto.Position;
                Employee.IsActive = true;
                Employee.IsLoggedIn = false;
                Employee.IsLoggedIn = false;
                Employee.AccessKey = "";

                _context.Employee.Add(Employee);
                await _context.SaveChangesAsync();
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

        public async Task<string> LoginEmployee(AuthanticationDTOs AuthanticationDTOs)
        {
            try
            {
                Log.Information("Starting Login");
                if (string.IsNullOrEmpty(AuthanticationDTOs.Email))
                    throw new Exception("Email Is Required");
                if (string.IsNullOrEmpty(AuthanticationDTOs.Password))
                    throw new Exception("Password Is Required");
                var Employee = _context.Employee.SingleOrDefault(x => x.Email.Equals(AuthanticationDTOs.Email) && x.Password.Equals(AuthanticationDTOs.Password));
                if (Employee != null)
                {
                    if (!Employee.IsLoggedIn)
                    {

                        //2-JWT 
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL");
                        var tokenDescriptior = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                        new Claim("UserId",Employee.EmployeeId.ToString()),
                        new Claim("Name",Employee.Name)
                            }),
                            Expires = DateTime.Now.AddHours(3),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptior);//encoding
                        string finalToken = tokenHandler.WriteToken(token);
                        Employee.IsLoggedIn = true;
                        Employee.AccessKey = finalToken;
                        Employee.AccesskeyExpireDate = tokenDescriptior.Expires;
                        _context.Employee.Update(Employee);
                        await _context.SaveChangesAsync();
                        Log.Information("LoginEmployee Is In Finised");
                        Log.Debug($"Debugging LoginEmployee Has been Finised Successfully With finalToken  {finalToken} ");


                        return finalToken;
                    }

                    else
                    {
                        Employee.IsLoggedIn = false;
                        _context.Update(Employee);
                        await _context.SaveChangesAsync();

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
        public async Task<string> DeleteEmployee(int id,[FromHeader] string email, [FromHeader] string password)
        {
           
            try
            {

                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }

                var employee= await _context.Employee.FindAsync(id);
                if (employee == null)
                {
                    Log.Error($"Employee Not Found ");
                    throw new ArgumentNullException("Employee", "Not Found Employee");
                }
                Log.Information("Employee Is In Existing");
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
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

        public async Task<List<EmployeeCardDTO>> GetAllEmployees([FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {

                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isAdminLoggedIn )
                {

                    throw new Exception("You Must Login In To Your Account");
                } 

                var Employees = await _context.Employee.ToListAsync();
                if (Employees == null)
                {
                    Log.Error($"Employees Not Found ");
                    throw new ArgumentNullException("Employees", "Not Found Employees");
                }
                var Result = from employeeDto in Employees
                             select new EmployeeCardDTO
                             {
                                    
                                 EmployeeId = employeeDto.EmployeeId,
                                 Name= employeeDto.Name,
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

        public async Task<EmployeeCardDTO> GetEmployeeById(int id, [FromHeader] string email, [FromHeader] string password)
        {
           
            try
            {
                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }


                var Employee = await _context.Employee.FirstOrDefaultAsync(x=>x.EmployeeId==id);
                if (Employee == null)
                {
                    Log.Error($"Employee Not Found ");
                    throw new ArgumentNullException("Employee", "Not Found Employee");
                }

                EmployeeCardDTO employeeCardDTO = new EmployeeCardDTO();
                employeeCardDTO.EmployeeId = Employee.EmployeeId;
                employeeCardDTO.Name = Employee.Name;
                employeeCardDTO.Email= Employee.Email;
                employeeCardDTO.Position = Employee.Position;
                employeeCardDTO.IsActive = Employee.IsActive;
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

        public async Task<string> UpdateEmployee(UpdateEmployeeDTO employeeDto,[FromHeader] string email, [FromHeader] string password)
        {
            
            try
            {
                var isAdminLoggedIn = await _context.Employee.AnyAsync(x => x.Email == email && x.Password == password && x.IsLoggedIn == true);
                if (!isAdminLoggedIn)
                {

                    throw new Exception("You Must Login In To Your Account");
                }


                var employee = await _context.Employee.FindAsync(employeeDto.EmployeeId);
                if (employee == null)
                {
                    Log.Error($"Employee Not Found ");
                    throw new ArgumentNullException("Employee", "Not Found");
                }
                Log.Information("Employee Is  Existing");
                employee.Name = employeeDto.Name;
                employee.Email = employeeDto.Email;
                employee.Password = employeeDto.Password;
                employee.Position = employeeDto.Position;
                employee.IsActive = employeeDto.IsActive;

                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();
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
    }
}
