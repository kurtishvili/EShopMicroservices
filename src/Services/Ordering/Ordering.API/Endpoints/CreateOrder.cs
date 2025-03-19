using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();

            var result = await sender.Send(command);

            var response = new CreateOrderResponse(result.OrderId);

            return Results.Created($"/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Create Order");
    }
}