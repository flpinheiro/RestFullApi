using RestFull.Domain.Core.Middlewares;
using RestFull.Domain.Infra.Configurations;
using RestFull.QueryService.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new NSwag.OpenApiInfo
        {
            Version = "v1",
            Title = "Query Api"
        };
    };
});

builder.Services.AddQueryServices();
builder.AddRepositoryService();

var app = builder.Build();

app.UseHttpsRedirection();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
