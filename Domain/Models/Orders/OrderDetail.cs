using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.Orders;

public class OrderDetail
{
    [JsonConstructor]
    public OrderDetail() { }

    public int Id { get; init; }
    public string ProductName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    [JsonIgnore]
    public Order Order { get; init; } = null!;

    public int OrderId { get; private set; }

    public static OrderDetail Create(string productName, int quantity, decimal price)
    {
        if (productName is { Length: <= 0 } || quantity < 0 || price < 0)
        {
            throw new ArgumentException("Invalid order details.", productName);
        }

        return new OrderDetail
        {
            ProductName = productName,
            Quantity = quantity,
            Price = price,
        };
    }
}
