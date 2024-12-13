using RestFull.Domain.Core.Dtos;
using RestFull.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFull.Domain.Core.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Guid> Add(Product product, CancellationToken cancellationToken);
    Task Update(Product product, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    //Task Rate(Guid id, int rate, CancellationToken cancellationToken);

    Task<Product> Get(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetPaginated(PaginatedQueryDto query, CancellationToken cancellationToken);
}
