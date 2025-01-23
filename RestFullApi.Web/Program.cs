using MudBlazor.Services;
using RestFullApi.Web.Components;
using RestFullApi.Web.Services;
using Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddRefitClient<IBFFApiService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https+http://bffApiService"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(cookie =>
{
    //Sets the cookie name and manage, so the cookie is invalidated.  
    cookie.Cookie.Name = "keycloak.cookie";
    cookie.Cookie.MaxAge = TimeSpan.FromMinutes(60);
    cookie.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    cookie.SlidingExpiration = true;
})
.AddOpenIdConnect(options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    options.Authority = builder.Configuration.GetValue<string>("Keycloak:RealmUrl");
    options.ClientId = builder.Configuration.GetValue<string>("Keycloak:ClientId");
    options.ClientSecret = builder.Configuration.GetValue<string>("Keycloak:ClientSecret");
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.ResponseType = OpenIdConnectResponseType.Code;
    // Disable HTTPS requirement for metadata in development
    options.RequireHttpsMetadata = false;
    options.CorrelationCookie.SameSite = SameSiteMode.Lax;
    options.NonceCookie.SameSite = SameSiteMode.Lax;
    //Save the token  
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("email");
    options.Scope.Add("phone");
    options.Scope.Add("profile");
    // options.NonceCookie.SameSite = SameSiteMode.None;
    // options.CorrelationCookie.SameSite = SameSiteMode.None;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        RoleClaimType = "https://schemas.scopic.com/roles"
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
