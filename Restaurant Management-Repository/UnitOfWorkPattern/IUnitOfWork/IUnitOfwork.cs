

using Restaurant_Management_Repository.IRepository;
using RestaurantManagement_Repository.IRepository;

namespace RestaurantManagement_Repository.UnitOfWorkPattern.IUnitOfWork
{
    public interface IUnitOfwork : IDisposable
    {

        IEmployeeOrderRepository _IEmployeeOrderRepository { get; }
        IAuthanticationRepository _IAuthanticationRepository { get; }
        ICustomerRepository _ICustomerRepository { get; }
        IEmployeeRepository _IEmployeeRepository { get; }
        IMenuRepository _IMenuRepository { get; }
        IOrderRepository _IOrderRepository { get; }

        ITableRepository _ITableRepository { get; }
      

              IOrderItemRepository _IOrderItemRepository { get; }
    Task<int> SaveChanges();

    }
}
