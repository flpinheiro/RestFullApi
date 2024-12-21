using RestFull.CommandService.Configurations;
using RestFull.Domain.Core.Middlewares;
using RestFull.Domain.Infra.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new NSwag.OpenApiInfo
        {
            Version = "v1",
            Title = "Command Api"
        };
    };
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddCommandServices();

builder.AddRepositoryService();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi();
}
else
{
    app.UseGlobalException();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

