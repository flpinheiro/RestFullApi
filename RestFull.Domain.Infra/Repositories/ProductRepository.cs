using Microsoft.EntityFrameworkCore;
using RestFull.Domain.Core.Entities;
using RestFull.Domain.Core.Interfaces.Repositories;
using RestFull.Domain.Core.Queries;
using RestFull.Domain.Infra.Contexts;

namespace RestFull.Domain.Infra.Repositories;

internal class ProductRepository(ApplicationDbContext Context) : IProductRepository
{
    public void Add(Product product, CancellationToken cancellationToken)
    {
        Context.Products.Add(product);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var product = await Context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null) throw new NotImplementedException();

        Context.Products.Remove(product);
    }


    public Task<Product?> Get(Guid id, CancellationToken cancellationToken) => Context.Products.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);


    public async Task<IEnumerable<Product>> Get(PaginatedQuery request, CancellationToken cancellationToken)
    {
        var query = Context.Products.AsQueryable();

        if (request.Query is not null)
            query = query.Where(a => a.Name.Contains(request.Query));

        return await query.OrderBy(p => p.Name).Skip((request.Page - 1) * request.Take).Take(request.Take).ToListAsync(cancellationToken);
    }

    public void Update(Product product, CancellationToken cancellationToken)
    {
        Context.Entry(product).State = EntityState.Modified;
    }

    public void Save()
    {
        Context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
