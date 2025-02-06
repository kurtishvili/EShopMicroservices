using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool isScucess);

public class UpdateOrder : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{id}", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();

            var result = await sender.Send(command);

            var response = new UpdateOrderResponse(result.isSuccess);

            return Results.Ok(response);
        })
       .WithName("UpdateOrder")
       .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithDescription("Update Order");
    }
}