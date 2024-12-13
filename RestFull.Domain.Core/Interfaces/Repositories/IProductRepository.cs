using RestFull.Domain.Core.Entities;
using RestFull.Domain.Core.Queries;

namespace RestFull.Domain.Core.Interfaces.Repositories;

public interface IProductRepository: IDisposable
{
    void Add(Product product, CancellationToken cancellationToken);
    void Update(Product product, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    //Task Rate(Guid id, int rate, CancellationToken cancellationToken);

    Task<Product> Get(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> Get(PaginatedQuery request, CancellationToken cancellationToken);

    void Save();
}
