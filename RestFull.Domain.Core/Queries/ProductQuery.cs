using MediatR;
using RestFull.Domain.Core.Entities;

namespace RestFull.Domain.Core.Queries;

public record ProductByIdQuery(Guid Id) : IRequest<Product>;

public class ProductPaginatedQuery : PaginatedQuery, IRequest<IEnumerable<Product>>
{
}