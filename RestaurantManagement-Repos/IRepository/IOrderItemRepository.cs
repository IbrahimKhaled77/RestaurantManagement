using RestaurantManagement.DTOs.OrderItemDTO;
using RestaurantManagement.Model.Entity;

namespace RestaurantManagement.IRepository
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItemCardDTO>> GetAllOrderItems();

        Task<OrderItem> GetOrderItemById(int OrderItemId);

        Task AddOrderItem(OrderItem OrderItem);

        Task UpdateOrderItem(OrderItem OrderItem);

        Task DeleteOrderItem(OrderItem OrderItem);

       


    }
}
