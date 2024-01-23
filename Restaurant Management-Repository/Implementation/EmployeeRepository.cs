

using Microsoft.EntityFrameworkCore;
using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.CustomerDTO;
using RestaurantManagement_Repository.DTOs.EmployeeDTO;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.Model.Entity;
using Serilog;
using System;

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

        public async Task<string> DeleteEmployee(int id)
        {
           
            try
            {
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

        public async Task<List<EmployeeCardDTO>> GetAllEmployees()
        {
            
            try
            {
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

        public async Task<EmployeeCardDTO> GetEmployeeById(int id)
        {
           
            try
            {
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

        public async Task<string> UpdateEmployee(UpdateEmployeeDTO employeeDto)
        {
            
            try
            {
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
