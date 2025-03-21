using Application.DTOs;
using AutoMapper;
using Domain.Models.Orders;

namespace Application.Mapper;

public class OrderResolver : IValueResolver<OrderRequest, Order, Order>
{
    public Order Resolve(
        OrderRequest source,
        Order destination,
        Order destMember,
        ResolutionContext context
    )
    {
        return Order.Create(source.CustomerName, source.OrderDetails);
    }
}

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        // Map OrderRequest -> Order using OrderResolver
        CreateMap<OrderRequest, Order>()
            .ForMember(dest => dest, opt => opt.MapFrom<OrderResolver>());

        // Map OrderDetailRequest -> OrderDetail using OrderDetailResolver
        CreateMap<OrderDetailRequest, OrderDetail>()
            .ForMember(dest => dest, opt => opt.MapFrom<OrderDetailResolver>());
    }
}
