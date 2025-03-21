using Application.DTOs;
using Domain.Models.Orders;
using MediatR;

namespace Application.Features.Orders.Commands;

public static class UpdateOrder
{
    public record Command(OrderRequest Order, int OrderId) : IRequest<Order>;

    public class Handler : IRequestHandler<Command, Order>
    {
        public Task<Order> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
