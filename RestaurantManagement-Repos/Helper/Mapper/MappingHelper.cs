

using RestaurantManagement.DTOs.EmployeeOrderDTO;
using RestaurantManagement.DTOs.OrderDTO;
using RestaurantManagement.DTOs.OrderItemDTO;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.Helper.Mapper
{
    public static class MappingHelper
    {

        // DTO order ticket data is stored in orders
        public static List<OrderCardDTO> TableDtoMapper(List<Order> Orders)
        {

            return Orders.Select(x => new OrderCardDTO
            {
                OrderId = x.OrderId,
                TotalPrice = x.TotalPrice,
                TableNumber = x.TableNumber,
                IsActive = x.IsActive,
                EmployeeOrder= EmployeeOrderDtoMapper(x.EmployeeOrder.ToList()),
                OrderItems= OrderItemDtoMapper(x.OrderItems.ToList()),

            }).ToList();

        }


      /*  public static OrderCardDTO orderoneDtoMapper(Order Orders)
        {
            OrderCardDTO order = new OrderCardDTO() { Is };
            return Orders.Select(x => new OrderCardDTO
            {
                OrderId = x.OrderId,
                TotalPrice = x.TotalPrice,
                TableNumber = x.TableNumber,
                IsActive = x.IsActive,
                EmployeeOrder = EmployeeOrderDtoMapper(x.EmployeeOrder.ToList()),

            }).ToList();

        }*/

        // DTO EmployeeOrder ticket data is stored in EmployeeOrders
        public static List<EmployeeOrderCardDTo> EmployeeOrderDtoMapper(List<EmployeeOrder> EmployeeOrders)
        {

            return EmployeeOrders.Select(x => new EmployeeOrderCardDTo
            {
                EmployeeOrderId = x.EmployeeOrderId,
                EmployeeId = x.EmployeeId,
                EmployeeName=x.Employee.Name,
                Position = x.Employee.Position,
                OrderId = x.OrderId,
                tableNumber=x.Order.TableNumber,
                TotalPrice=x.Order.TotalPrice, 
                
                 


            }).ToList();

        }

        // DTO OrderItem ticket data is stored in orderItems
        public static List<OrderItemCardDTO> OrderItemDtoMapper(List<OrderItem> orderItems)
        {

            return orderItems.Select(x => new OrderItemCardDTO
            {
                OrderItemId = x.OrderItemId,
                MenuId = x.MenuId,
                MenuName=x.Menu.Name,
                price=x.Menu.Price,
                OrderId = x.OrderId,
                Quantity = x.Quantity,
                IsActive = x.IsActive,
              



            }).ToList();

        }








    }
}

