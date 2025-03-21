using Domain.Models.Orders;
using MediatR;

namespace Application.Features.Orders.Queries;

public static class GetAllOrders
{
    public record Query() : IRequest<List<Order>>;

    public class Handler : IRequestHandler<Query, List<Order>>
    {
        public Task<List<Order>> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
