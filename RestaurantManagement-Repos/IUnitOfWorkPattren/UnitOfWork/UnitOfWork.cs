using RestaurantManagement.Context;
using RestaurantManagement.IRepository;
using RestaurantManagement.UnitOfWorkPattern.IUnitOfWork;

namespace RestaurantManagement.UnitOfWorkPattern.UnitOfWork
{
    public class UnitOfWork : IUnitOfwork
{
        private readonly RestaurantManagemenstContext _context;

        public IEmployeeOrderRepository EmployeeOrderRepository { get;private set; }

        public IAuthanticationRepository AuthanticationRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }

        public IEmployeeRepository EmployeeRepository { get; private set; }

        public IMenuRepository MenuRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public ITableRepository TableRepository { get; private set; }


        public IOrderItemRepository OrderItemRepository { get; private set; }

        public UnitOfWork(RestaurantManagemenstContext context, IOrderItemRepository orderItemRepository, ITableRepository tableRepository, IOrderRepository orderRepository, IMenuRepository menuRepository, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IAuthanticationRepository authanticationRepository, IEmployeeOrderRepository employeeOrderRepository)
        {
            _context = context;
            TableRepository = tableRepository;
            OrderRepository = orderRepository;
            MenuRepository = menuRepository;
            EmployeeRepository = employeeRepository;
            CustomerRepository = customerRepository;
            AuthanticationRepository = authanticationRepository;
            EmployeeOrderRepository = employeeOrderRepository;
            OrderItemRepository = orderItemRepository;
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
