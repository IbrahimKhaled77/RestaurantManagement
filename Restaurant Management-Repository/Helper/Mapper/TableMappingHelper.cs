

using RestaurantManagement_Repository.DTOs.EmployeeOrderDTO;
using RestaurantManagement_Repository.DTOs.OrderDTO;
using RestaurantManagement_Repository.DTOs.OrderItemDTO;
using RestaurantManagement_Repository.Model.Entity;

namespace RestaurantManagement_Repository.Helper.Mapper
{
    public static class TableMappingHelper
    {

        public static List<OrderCardDTO> TableDtoMapper(List<Order> Orders) { 
        
            return Orders.Select(x=>new OrderCardDTO {
                OrderId=x.OrderId,
                TotalPrice=x.TotalPrice,
                TableNumber=x.TableNumber
            }).ToList();

        }
        public static List<EmployeeOrderCardDTo> EmployeeOrderDtoMapper(List<EmployeeOrder> EmployeeOrders)
        {

            return EmployeeOrders.Select(x => new EmployeeOrderCardDTo
            {
                EmployeeOrderId = x.EmployeeOrderId,
                EmployeeId =x.EmployeeId,
                 OrderId=x.OrderId,
              

            }).ToList();

        }

        public static List<OrderItem> OrdersDtoMapper(List<CreateOrderItemDTO> Orders)
        {
            List<OrderItem> OrderItem = new List<OrderItem>();
            foreach (CreateOrderItemDTO Order in Orders)
            {
                OrderItem dTO = new OrderItem();
                dTO.OrderId = 20;
                dTO.Quantity = Order.Quantity;
                dTO.MenuId= Order.MenuId;
                dTO.IsActive = Order.IsActive;
                OrderItem.Add(dTO);
            }
            return OrderItem;
        }


      /*  public static List<CreateOrderItemDTO> OrderDtoMapper(List<OrderItem> Orders)
        {
            List<CreateOrderItemDTO> createOrderItemDTOs = new List<CreateOrderItemDTO>();
            foreach (Order Order in Orders)
            {
                CreateOrderItemDTO dTO = new CreateOrderItemDTO();
               
                dTO.Quantity = Order.;
                dTO.QuestionCount = exam.QuestionCount;
                dTO.Duration = exam.Duration;
                dTO.Level = exam.level;
                dTO.ActualMark = exam.ActualMark;
                dTO.ExamId = exam.ExamId;
                examDTOs.Add(dTO);
            }
            return examDTOs;
        }*/



    }
}
