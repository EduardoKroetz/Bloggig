using Bloggig.Infra.Persistance;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

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
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
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
        options.CallbackPath = "/api/Auth/oauth/callback";
    });
}