using System.ComponentModel.DataAnnotations;
using Domain.Models.Orders;

namespace Application.DTOs;

public record OrderRequest(
    [StringLength(255)] string CustomerName,
    List<OrderDetail> OrderDetails,
    OrderStatus OrderStatus
);
