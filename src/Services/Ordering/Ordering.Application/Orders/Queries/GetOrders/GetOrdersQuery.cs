using BuildingBlocks.Pagination;

public record GetOrdersQuery(PationationRequest paginationRequest) : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);