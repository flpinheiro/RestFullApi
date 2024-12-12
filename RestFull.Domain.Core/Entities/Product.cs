namespace RestFull.Domain.Core.Entities;

[ExcludeFromCodeCoverage]
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; } 
    public decimal Billing { get; set; }
    public int Quantity { get; set; }
}
