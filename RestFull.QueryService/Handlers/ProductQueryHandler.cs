using MediatR;
using RestFull.Domain.Core.Entities;
using RestFull.Domain.Core.Interfaces;
using RestFull.Domain.Core.Queries;

namespace RestFull.QueryService.Handlers;

public sealed class ProductQueryHandler(IUnitOfWork unit) : IRequestHandler<ProductByIdQuery, Product>,
    IRequestHandler<ProductPaginatedQuery, IEnumerable<Product>>
{
    Task<Product> IRequestHandler<ProductByIdQuery, Product>.Handle(ProductByIdQuery request, CancellationToken cancellationToken) => unit.ProductRepository.GetAsync(request.Id, cancellationToken);

    Task<IEnumerable<Product>> IRequestHandler<ProductPaginatedQuery, IEnumerable<Product>>.Handle(ProductPaginatedQuery request, CancellationToken cancellationToken) => unit.ProductRepository.GetAsync(request, cancellationToken);
}
