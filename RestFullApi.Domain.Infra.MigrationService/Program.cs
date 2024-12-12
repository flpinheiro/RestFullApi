using RestFull.Domain.Infra.Contexts;
using RestFullApi.Domain.Infra.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<CommandDbContext>("database");

var host = builder.Build();
host.Run();
