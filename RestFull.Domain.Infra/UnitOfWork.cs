﻿using RestFull.Domain.Core.Interfaces;
using RestFull.Domain.Core.Interfaces.Repositories;
using RestFull.Domain.Infra.Contexts;
using RestFull.Domain.Infra.Repositories;

namespace RestFull.Domain.Infra;

public class UnitOfWork(ApplicationDbContext Context) : IUnitOfWork
{
    private IProductRepository? _productRepository;

    public IProductRepository ProductRepository
    {
        get
        {
            if (_productRepository is null)
                _productRepository = new ProductRepository(Context);
            return _productRepository;
        }
        set { _productRepository = value; }
    }

    public async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Context.Dispose();
                _productRepository = null;
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
