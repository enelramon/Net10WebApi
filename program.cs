#:sdk Microsoft.NET.Sdk.Web
#:package Microsoft.AspNetCore.OpenApi@10.*-*
#:package Microsoft.AspNetCore.Authentication.JwtBearer@8.*-*
#:package Microsoft.Extensions.Options@8.*-*
#:package Microsoft.IdentityModel.Tokens@8.*-*

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["Jwt:Issuer"] = "https://tu-dominio.com",
    ["Jwt:Audience"] = "https://tu-dominio.com",
    ["Jwt:Key"] = "ESTA_ES_UNA_CLAVE_SECRETA_MUY_LARGA_Y_SEGURA_CAMBIALA",
    ["ApiKey"] = "ESTA_ES_MI_API_KEY_ULTRA_SECRETA_PARA_SERVICIOS"
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

builder.Services.AddAuthentication(options => { })
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = config["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    })
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthHandler>("ApiKey", null);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseOpenApi();
app.UseSwaggerUi();

// Público
app.MapGet("/api/data/public", () => Results.Ok("Estos son datos públicos."));

// JWT SOLAMENTE
app.MapGet("/api/data/user-data", (ClaimsPrincipal user) =>
{
    var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Desconocido";
    return Results.Ok($"Estos son datos privados para el usuario: {username}");
})
.RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme });

// API KEY SOLAMENTE
app.MapGet("/api/data/service-data", () =>
    Results.Ok("Estos son datos solo para clientes con API Key.")
)
.RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = "ApiKey" });

// JWT O API KEY
app.MapGet("/api/data/flexible-data", () =>
    Results.Ok("Estos datos son accesibles con JWT o con API Key.")
)
.RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},ApiKey" });

app.Run();


// --- AUTH HANDLER API KEY ---
public class ApiKeyAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly string _apiKey;
    
    public ApiKeyAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _apiKey = configuration["ApiKey"]!;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        
        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
        if (string.IsNullOrEmpty(providedApiKey) || !providedApiKey.Equals(_apiKey))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key provided."));
        }
        
        var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}