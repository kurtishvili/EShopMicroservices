using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool isSuccess);

public class DeleteOrder : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteOrderCommand(id);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
         .WithName("DeleteOrder")
         .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithDescription("Delete Order");
    }
}