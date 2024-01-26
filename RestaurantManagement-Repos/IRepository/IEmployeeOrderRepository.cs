using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface IEmployeeOrderRepository
    
    {
      
        Task AddEmployeeOrder(EmployeeOrder employeeOrder);
        Task SaveChangesAsync();
    }
}
