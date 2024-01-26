





using RestaurantManagement.IRepository;

namespace RestaurantManagement.UnitOfWorkPattern.IUnitOfWork
{
    public interface IUnitOfwork : IDisposable
    {

        IEmployeeOrderRepository EmployeeOrderRepository{ get; }
        IAuthanticationRepository AuthanticationRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IMenuRepository MenuRepository { get; }
        IOrderRepository OrderRepository { get; }

        ITableRepository TableRepository { get; }


        IOrderItemRepository OrderItemRepository{ get; }

        Task<int> SaveChanges();

    }
}
