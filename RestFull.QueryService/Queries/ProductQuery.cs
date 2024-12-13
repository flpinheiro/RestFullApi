using MediatR;
using RestFull.Domain.Core.Dtos;
using RestFull.Domain.Core.Entities;

namespace RestFull.QueryService.Queries;

public record ProductByIdQuery(Guid Id): IRequest<Product>;

public class ProductPaginatedQuery : PaginatedQueryDto, IRequest<IEnumerable<Product>>
{
}