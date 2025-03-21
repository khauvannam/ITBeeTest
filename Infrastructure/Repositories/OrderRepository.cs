using Application.Interfaces.Repositories;
using Domain.Models.Orders;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _dbContext;

    public OrderRepository(StoreDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        var order = await _dbContext
            .Orders.Include(o => o.OrderDetails)
            .AsSplitQuery()
            .FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is null)
        {
            throw new KeyNotFoundException($"Order with Id {orderId} not found.");
        }

        return order;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _dbContext.Orders.Include(o => o.OrderDetails).AsSplitQuery().ToListAsync();
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order, int orderId)
    {
        var oldOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (oldOrder is not null)
        {
            oldOrder.Update(order.CustomerName, order.Status);
            return oldOrder;
        }

        throw new KeyNotFoundException($"Order with Id {orderId} not found.");
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is not null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        throw new KeyNotFoundException($"Order with Id {orderId} not found.");
    }

    public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
    {
        var orderDetails = await _dbContext.OrderDetails.Where(o => o.Id == orderId).ToListAsync();
        return orderDetails;
    }

    public async Task<Order> CreateOrderDetailAsync(OrderDetail orderDetail, int orderId)
    {
        var order = await _dbContext
            .Orders.Include(o => o.OrderDetails)
            .AsSplitQuery()
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is not null)
        {
            order.AddOrderDetail(orderDetail);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        throw new KeyNotFoundException($"Order with Id {orderDetail.Id} not found.");
    }

    public async Task DeleteOrderDetailAsync(int orderDetailId)
    {
        var orderDetail = _dbContext.OrderDetails.FirstOrDefault(o => o.Id == orderDetailId);

        if (orderDetail is not null)
        {
            _dbContext.OrderDetails.Remove(orderDetail);
            await _dbContext.SaveChangesAsync();
        }

        throw new KeyNotFoundException($"Order with Id {orderDetailId} not found.");
    }
}
