using System.Text.Json.Serialization;

namespace Domain.Models.Orders;

public class Order
{
    [Newtonsoft.Json.JsonConstructor]
    private Order() { }

    [JsonInclude]
    public int Id { get; init; }

    [JsonInclude]
    public string CustomerName { get; private set; } = null!;

    [JsonInclude]
    public decimal TotalAmount { get; private set; }
    public DateTime CreateAt { get; } = DateTime.Now;
    public DateTime UpdateAt { get; private set; } = DateTime.Now;

    [JsonInclude]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [JsonInclude]
    public List<OrderDetail> OrderDetails { get; init; } = null!;

    public static Order Create(string customerName, List<OrderDetail> orderDetails)
    {
        if (orderDetails is not { Count: > 0 })
        {
            throw new ArgumentException("Order details cannot be empty.", nameof(orderDetails));
        }

        return new Order() { CustomerName = customerName, OrderDetails = orderDetails };
    }

    public Order Update(string? customerName, OrderStatus status = OrderStatus.Pending)
    {
        if (customerName is { Length: > 0 })
        {
            CustomerName = customerName!;
        }

        Status = status;
        UpdateAt = DateTime.Now;
        return this;
    }

    public Order AddOrderDetail(OrderDetail orderDetail)
    {
        OrderDetails.Add(orderDetail);
        UpdateAt = DateTime.Now;
        TotalAmount += orderDetail.Quantity * orderDetail.Price;
        return this;
    }

    public Order RemoveOrderDetail(int orderDetailId)
    {
        var orderDetail = OrderDetails.FirstOrDefault(od => od.Id == orderDetailId);
        if (orderDetail is null)
        {
            throw new KeyNotFoundException($"OrderDetail with Id {orderDetailId} not found.");
        }

        TotalAmount -= orderDetail.Quantity * orderDetail.Price;
        return this;
    }
}

public enum OrderStatus
{
    Pending,
    Completed,
    Canceled,
}
