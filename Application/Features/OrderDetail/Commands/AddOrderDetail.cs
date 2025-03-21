using Application.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Models.Orders;
using MediatR;

namespace Application.Features.OrderDetail.Commands;

public static class AddOrderDetail
{
    public record Command(int OrderId, OrderDetailRequest OrderDetail) : IRequest<Order>;

    public class Handler : IRequestHandler<Command, Order>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;

        public Handler(IOrderRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Order> Handle(Command request, CancellationToken cancellationToken)
        {
            var orderDetail = _mapper.Map<Domain.Models.Orders.OrderDetail>(request.OrderDetail);
            return await _repository.CreateOrderDetailAsync(orderDetail, request.OrderId);
        }
    }
}
