using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.OrderDetail.Queries;

public static class GetOrderDetails
{
    public record Query(int OrderId) : IRequest<List<Domain.Models.Orders.OrderDetail>>;

    public class Handler : IRequestHandler<Query, List<Domain.Models.Orders.OrderDetail>>
    {
        private readonly IOrderRepository _repository;

        public Handler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Domain.Models.Orders.OrderDetail>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetOrderDetailsAsync(request.OrderId);
        }
    }
}
