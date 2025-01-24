using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddOcelotService(builder.Configuration);
builder.Services.AddSwaggerService(builder.Configuration);

builder.Services.AddAuthentication()
                .AddKeycloakJwtBearer("keycloak", realm: "ReastApi", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = "weather.api";
                });

builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerMiddlare();
}

await app.UseOcelot();

app.MapControllers();

app.Run();