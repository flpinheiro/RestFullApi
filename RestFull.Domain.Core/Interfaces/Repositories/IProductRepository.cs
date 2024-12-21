using RestFull.Domain.Core.Entities;
using RestFull.Domain.Core.Queries;

namespace RestFull.Domain.Core.Interfaces.Repositories;

public interface IProductRepository
{
    void Add(Product product, CancellationToken cancellationToken);
    void Update(Product product, CancellationToken cancellationToken);
    void Delete(Product product);
    Task<Product> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAsync(PaginatedQuery request, CancellationToken cancellationToken);
}
