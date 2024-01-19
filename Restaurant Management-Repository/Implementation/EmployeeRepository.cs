

using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.DTOs.EmployeeDTO;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement_Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantManagementContext _context;

        public EmployeeRepository(RestaurantManagementContext context)
        {
            _context = context;
        }
        public Task<bool> AddEmployee(CreatEmployeeDTO employee)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<List<EmployeeCardDTO>> GetAllEmployees()
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<EmployeeCardDTO> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        public Task<bool> UpdateEmployee(UpdateEmployeeDTO employee)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch (Exception ex)
            {
            }
        }
    }
}
