using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOcelotService(builder.Configuration);
builder.Services.AddSwaggerService(builder.Configuration);

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

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerMiddlare();
}

await app.UseOcelot();

app.MapControllers();

app.Run();