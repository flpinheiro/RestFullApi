using MediatR;
using RestFull.CommandService.Commands;

namespace RestFull.CommandService.Handlers;

public sealed class ProductsCommandHandler : IRequestHandler<UpdateProductCommand>, IRequestHandler<AddProductCommand, Guid>, IRequestHandler<DeleteProductCommand>, IRequestHandler<RateProductCommand>
{
    Task IRequestHandler<UpdateProductCommand>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<Guid> IRequestHandler<AddProductCommand, Guid>.Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task IRequestHandler<RateProductCommand>.Handle(RateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
