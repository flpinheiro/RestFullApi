using Microsoft.EntityFrameworkCore;
using RestFull.Domain.Infra.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<CommandDbContext>(connectionName: "database");

//builder.Services.AddDbContextPool<CommandDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("database"), sqlOptions =>
//    {
//        // Workaround for https://github.com/dotnet/aspire/issues/1023
//        //sqlOptions.ExecutionStrategy(c => new RetryingSqlServerRetryingExecutionStrategy(c));
//        sqlOptions.MigrationsAssembly("RestFull.Domain.Infra.MigrationWorker");
//    }));

builder.EnrichSqlServerDbContext<CommandDbContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new NSwag.OpenApiInfo
        {
            Version = "v1",
            Title = "Weather Forecast Api"
        };
    };
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
