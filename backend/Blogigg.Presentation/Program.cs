using Bloggig.Application.Services;
using Bloggig.Domain.Repositories;
using Bloggig.Domain.Services;
using Bloggig.Infra.Persistance;
using Bloggig.Infra.Persistance.Repositories;
using Bloggig.Infra.Services;
using Bloggig.Infra.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IGoogleApiService, GoogleApiService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//Adiciona DbContext e a conexão com o SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found");
builder.Services.AddDbContext<BloggigDbContext>(options => {
    options.UseSqlServer(connectionString);
});

//Configura autenticação
LoadAutheticationConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())   
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void LoadAutheticationConfig()
{
    var googleClientId = builder.Configuration.GetValue<string>("GoogleClientId") ?? throw new Exception("Google client id not found");
    var googleClientSecret = builder.Configuration.GetValue<string>("GoogleClientSecret") ?? throw new Exception("Google client secret not found");
    var jwtKey = builder.Configuration.GetValue<string>("JwtKey") ?? throw new Exception("Jwt key not found");
    var validIssuer = builder.Configuration.GetValue<string>("JwtIssuer") ?? throw new Exception("Issuer not found");
    var validAudience = builder.Configuration.GetValue<string>("JwtAudience") ?? throw new Exception("Audience not found");
    var frontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? throw new Exception("Frontend Url not found");
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; //Esquema que vai usar para fazer login quando o usuário não estiver logado
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
    })
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Events.OnValidatePrincipal = async context =>
        {
            var accessToken = context.Principal.FindFirst("access_token")?.Value;

            if (accessToken != null)
            {
                context.Properties.Items["access_token"] = accessToken;
            }
        };
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.Redirect($"/login.html"); //Redirecionar o usuário para tela de login no frontend
            return Task.CompletedTask;
        };
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    })
    .AddGoogle(options =>
    {
        options.ClientId = googleClientId;
        options.ClientSecret = googleClientSecret;
        options.CallbackPath = "/api/OAuth/google/internal-callback";
        options.Events.OnCreatingTicket = context =>
        {
            context.Properties.Items["access_token"] = context.AccessToken;
            return Task.CompletedTask;
        };
    });
}