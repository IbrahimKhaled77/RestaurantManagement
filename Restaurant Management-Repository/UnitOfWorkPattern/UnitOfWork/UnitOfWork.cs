

using RestaurantManagement_Repository.Context;
using RestaurantManagement_Repository.IRepository;
using RestaurantManagement_Repository.UnitOfWorkPattern.IUnitOfWork;

namespace RestaurantManagement_Repository.UnitOfWorkPattern.UnitOfWork
{
    public class UnitOfWork : IUnitOfwork



    {
        private readonly RestaurantManagementContext _context;

        public IEmployeeOrderRepository _IEmployeeOrderRepository { get;private set; }

        public IAuthanticationRepository _IAuthanticationRepository { get; private set; }

        public ICustomerRepository _ICustomerRepository { get; private set; }

        public IEmployeeRepository _IEmployeeRepository { get; private set; }

        public IMenuRepository _IMenuRepository { get; private set; }

        public IOrderRepository _IOrderRepository { get; private set; }

        public ITableRepository _ITableRepository { get; private set; }

        public UnitOfWork(RestaurantManagementContext context, ITableRepository tableRepository, IOrderRepository orderRepository, IMenuRepository menuRepository, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IAuthanticationRepository authanticationRepository, IEmployeeOrderRepository employeeOrderRepository)
        {
            _context = context;
            _ITableRepository = tableRepository;
            _IOrderRepository = orderRepository;
            _IMenuRepository = menuRepository;
            _IEmployeeRepository = employeeRepository;
            _ICustomerRepository = customerRepository;
            _IAuthanticationRepository = authanticationRepository;
            _IEmployeeOrderRepository= employeeOrderRepository;
        }

     

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
