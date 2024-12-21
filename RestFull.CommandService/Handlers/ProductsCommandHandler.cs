using MediatR;
using RestFull.Domain.Core.Commands;
using RestFull.Domain.Core.Entities;
using RestFull.Domain.Core.Interfaces;

namespace RestFull.CommandService.Handlers;

public sealed class ProductsCommandHandler(IUnitOfWork unit) : IRequestHandler<UpdateProductCommand>, IRequestHandler<AddProductCommand, Guid>, IRequestHandler<DeleteProductCommand>, IRequestHandler<RateProductCommand>
{
    async Task IRequestHandler<UpdateProductCommand>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unit.ProductRepository.GetAsync(request.Id, cancellationToken);
        
        if(product is null) 
        {
            //TODO IMPLEMENT
            throw new NotImplementedException(); 
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Billing = request.Billing;
        product.Quantity = request.Quantity;

        unit.ProductRepository.Update(product, cancellationToken);

        await unit.SaveAsync();
    }

    async Task<Guid> IRequestHandler<AddProductCommand, Guid>.Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = request.Name,
            Description = request.Description,
            Billing = request.Billing,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        unit.ProductRepository.Add(product, cancellationToken);

        await unit.SaveAsync();

        return product.Id ?? Guid.Empty;
    }

    async Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unit.ProductRepository.GetAsync(request.id, cancellationToken);
        if(product is null ) 
        {
            //TODO IMPLEMENT
            throw new NotImplementedException(); 
        }
        unit.ProductRepository.Delete(product);
        await unit.SaveAsync();
    }

    Task IRequestHandler<RateProductCommand>.Handle(RateProductCommand request, CancellationToken cancellationToken)
    {
        //TODO IMPLEMENT
        throw new NotImplementedException();
    }
}
