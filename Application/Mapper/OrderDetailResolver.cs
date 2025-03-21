using Application.DTOs;
using Domain.Models.Orders;

namespace Application.Mapper;

using AutoMapper;

public class OrderDetailResolver : IValueResolver<OrderDetailRequest, OrderDetail, OrderDetail>
{
    public OrderDetail Resolve(
        OrderDetailRequest source,
        OrderDetail destination,
        OrderDetail destMember,
        ResolutionContext context
    )
    {
        return OrderDetail.Create(source.ProductName, source.Quantity, source.Price);
    }
}
