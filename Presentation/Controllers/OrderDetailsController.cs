using Application.DTOs;
using Application.Features.OrderDetail.Commands;
using Application.Features.OrderDetail.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/orders/{id:int}/order-details")]
[ApiController]
public class OrderDetailsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderDetailsController> _logger;

    public OrderDetailsController(IMediator mediator, ILogger<OrderDetailsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddOrderDetail(int id, [FromBody] OrderDetailRequest request)
    {
        _logger.LogInformation(
            "Adding product {ProductName} to order {OrderId}",
            request.ProductName,
            id
        );
        var orderDetail = await _mediator.Send(new AddOrderDetail.Command(id, request));
        return CreatedAtAction(nameof(GetOrderDetails), new { id }, orderDetail);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        _logger.LogInformation("Fetching order details for order {OrderId}", id);
        var orderDetails = await _mediator.Send(new GetOrderDetails.Query(id));
        return Ok(orderDetails);
    }

    [HttpDelete("/order-details/{id:int}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        await _mediator.Send(new DeleteOrderDetail.Command(id));

        return NoContent();
    }
}
