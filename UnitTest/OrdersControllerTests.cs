using Application.Features.Orders.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Presentation.Controllers;

namespace UnitTest;

public class OrdersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OrdersController _controller;

    public OrdersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        var loggerMock = new Mock<ILogger<OrdersController>>();
        _controller = new OrdersController(_mediatorMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnNoContent()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteOrder.Command>(), default))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteOrder(1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteOrder_InvalidId_ShouldReturnNotFound()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteOrder.Command>(), default))
            .ThrowsAsync(new KeyNotFoundException("Order not found"));

        // Act
        var result = await _controller.DeleteOrder(99);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }
}
