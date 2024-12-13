using RestFull.Domain.Core.Interfaces.Repositories;

namespace RestFull.Domain.Core.Interfaces;

public interface IUnitOfWork
{
    IProductRepository  ProductRepository { get; set; }

    Task SaveAsync();
}
