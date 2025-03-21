using MediatR;

namespace Application.Features.Orders.Commands;

public static class DeleteOrder
{
    public record Command(int OrderId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        public Task Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
