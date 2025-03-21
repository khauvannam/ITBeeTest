using Domain.Models.Orders;
using MediatR;

namespace Application.Features.Orders.Queries;

public static class GetOrderById
{
    public record Query(int OrderId) : IRequest<Order>;

    public class Handler : IRequestHandler<Query, Order>
    {
        public Task<Order> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
