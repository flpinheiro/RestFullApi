using MediatR;

namespace RestFull.Domain.Core.Commands;

public class AddProductCommand : IRequest<Guid>
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public decimal Price { get; init; }

    public decimal Billing { get; init; }

    public int Quantity { get; init; }
}

public class UpdateProductCommand : AddProductCommand, IRequest
{
    public Guid Id { get; init; }
}

public record DeleteProductCommand(Guid id) : IRequest;
public record RateProductCommand(Guid Id, int Rate) : IRequest;
