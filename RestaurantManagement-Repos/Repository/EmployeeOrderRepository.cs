
using RestaurantManagement.Context;
using RestaurantManagement.IRepository;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Implementation
{
    public class EmployeeOrdersRepository : IEmployeeOrderRepository
    {
        private readonly RestaurantManagemenstContext _context;

        public EmployeeOrdersRepository(RestaurantManagemenstContext context)
        {
            _context = context;
        }


        //Add EmployeeOrder in Database in table EmployeeOrder
        public async Task AddEmployeeOrder(EmployeeOrder employeeOrder)
        {
            _context.EmployeeOrder.Add(employeeOrder);
            await _context.SaveChangesAsync();
        }

        //Save any changes made
        public async Task SaveChangesAsync() {

            await _context.SaveChangesAsync();
        }


    }
}
