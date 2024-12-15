using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestFull.Domain.Core.Interfaces;
using RestFull.Domain.Infra.Contexts;

namespace RestFull.Domain.Infra.Configurations;

public static class RepositoryServiceRegistration
{
    public static void AddRepositoryService(this WebApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<ApplicationDbContext>(connectionName: "database");
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
