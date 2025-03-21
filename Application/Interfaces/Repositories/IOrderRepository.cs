using Domain.Models.Orders;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetOrdersAsync();
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Order order, int orderId);
    Task DeleteOrderAsync(int orderId);
    Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId);
    Task<Order> CreateOrderDetailAsync(OrderDetail orderDetail, int orderId);
    Task DeleteOrderDetailAsync(int orderDetailId);
}
