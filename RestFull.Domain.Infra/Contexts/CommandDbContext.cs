using Microsoft.EntityFrameworkCore;
using RestFull.Domain.Core.Entities;
using System.Dynamic;

namespace RestFull.Domain.Infra.Contexts;

public sealed class CommandDbContext(DbContextOptions<CommandDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
