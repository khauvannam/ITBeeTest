using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.OrderDetail.Commands;

public static class DeleteOrderDetail
{
    public record Command(int OrderDetailId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IOrderRepository _repository;

        public Handler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _repository.DeleteOrderDetailAsync(request.OrderDetailId);
        }
    }
}
