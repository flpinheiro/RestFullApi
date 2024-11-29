using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOcelotService(builder.Configuration);
builder.Services.AddSwaggerService(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerMiddlare();
}

await app.UseOcelot();

app.MapControllers();

app.Run();