using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using RestFull.Domain.Infra.Contexts;

namespace RestFull.Domain.Infra.Configurations;

public static class RepositoryServiceRegistration
{
    public static void AddRepositoryService(this WebApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<CommandDbContext>(connectionName: "database");
        builder.EnrichSqlServerDbContext<CommandDbContext>();
    }
}
