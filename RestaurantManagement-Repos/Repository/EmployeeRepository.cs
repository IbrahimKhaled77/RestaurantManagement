using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantManagement.Context;
using RestaurantManagement.DTOs.EmployeeDTO;
using RestaurantManagement.Helper.Mapper;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;


namespace RestaurantManagement.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantManagemenstContext _context;

        public EmployeeRepository(RestaurantManagemenstContext context)
        {
            _context = context;
        }

        // Get All of List Customer
        public async Task<List<EmployeeCardDTO>> GetAllEmployees()
        {
            return await _context.Employee.Include(x => x.EmployeeOrder).ThenInclude(x=>x.Order).Include(c=>c.EmployeeOrder).ThenInclude(c=>c.Employee).Select(x=>new EmployeeCardDTO {
                EmployeeId = x.EmployeeId,
                Name = x.Name,
                Position = x.Position,
                Email = x.Email,
                IsActive = x.IsActive,
                EmployeeOrders=  MappingHelper.EmployeeOrderDtoMapper( x.EmployeeOrder.ToList()),
            
            
            }).ToListAsync();

        }

        // Get Employee By Employee Id
        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            var employee= await _context.Employee.Where(x => x.EmployeeId == employeeId).Include(x => x.EmployeeOrder).ThenInclude(x => x.Order).Include(c => c.EmployeeOrder).SingleAsync();

            return employee!;

        }

        //Add Employee in Database in Table Employee 
        public async Task AddEmployee(Employee Employee)
        {
            _context.Employee.Add(Employee);
            await _context.SaveChangesAsync();

        }

        //Update Employee in Database in Table Employee 
        public async Task UpdateEmployee(Employee Employee)
        {

            _context.Employee.Update(Employee);
            await _context.SaveChangesAsync();
        }

        //Delete Employee in Database in Table Employee 
        public async Task DeleteEmployee(Employee Employee)
        {
            _context.Employee.Remove(Employee);
            await _context.SaveChangesAsync();

        }

        //Enter the Employee email and password and search for them in the Employee table in Database
        public async Task<Employee> LoginEmployee(string Email, string Password)
        {
          var employee =  await _context.Employee.SingleOrDefaultAsync(x => x.Email.Equals(Email) && x.Password.Equals(Password));
            return employee!;
        }

        //Enter the employee's email and password and search for them in the employees table in the database and
        //find out whether the employee is active or not and it returns true or false and you must enter the job.
        public async Task<bool> IsEmployeeLoggedIn(string Email, string Password, string Position)
        {
            return await _context.Employee.AnyAsync(x => x.Email == Email && x.Password == Password && x.IsLoggedIn == true && x.Position == Position);
        }

        //Enter the Employee's email and password and search for them in the Employee table in the database and
        //see if the Employee is active or not and it returns true or false.
        public async Task<bool> IsEmployeeLoggedIn(string Email, string Password)
        {
            return await _context.Employee.AnyAsync(x => x.Email == Email && x.Password == Password && x.IsLoggedIn == true);
        }

        //Enter the employee's email and password and search for them in the employees table in the database and
        //find out whether the employee is Not active or not and it returns true or false and you must enter the job.
        public async Task<bool> IsEmployeeLoggedAdminIn(string Email, string Password, string Position)
        {
            return await _context.Employee.AnyAsync(x => x.Email == Email && x.Password == Password && x.Position == Position);
        }



    }
}
