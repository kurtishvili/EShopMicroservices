using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

internal class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
            .Take(query.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken);

        var totalOrders = await dbContext.Orders.LongCountAsync(cancellationToken);

        return new GetOrdersResult(new PaginatedResult<OrderDto>(query.PaginationRequest.PageIndex, query.PaginationRequest.PageSize, totalOrders, orders.ToOrderDtoList()));
    }
}
