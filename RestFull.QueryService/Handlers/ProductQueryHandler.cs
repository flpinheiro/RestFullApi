using MediatR;
using RestFull.Domain.Core.Entities;
using RestFull.QueryService.Queries;

namespace RestFull.QueryService.Handlers;

public sealed class ProductQueryHandler : IRequestHandler<ProductByIdQuery, Product>,
    IRequestHandler<ProductPaginatedQuery, IEnumerable<Product>>
{
    Task<Product> IRequestHandler<ProductByIdQuery, Product>.Handle(ProductByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Product>> IRequestHandler<ProductPaginatedQuery, IEnumerable<Product>>.Handle(ProductPaginatedQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
