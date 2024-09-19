using Bloggig.Application.Services;
using Bloggig.Application.Services.Interfaces;
using Bloggig.Domain.Repositories;
using Bloggig.Infra.Persistance;
using Bloggig.Infra.Persistance.Repositories;
using Bloggig.Infra.Services;
using Bloggig.Infra.Services.Interfaces;
using Bloggig.Presentation.Exception;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    {
        AbortOnConnectFail = false, 
        ConnectTimeout = 5000, 
        EndPoints = { "redis:6379" }
    };
});

builder.Services.Configure<ApiBehaviorOptions>(options => 
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
builder.Services.AddScoped<IGoogleApiService, GoogleApiService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserTagPointsRepository, UserTagPointsRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserTagPointsService, UserTagPointsService>();

var frontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? throw new System.Exception("Frontend Url not found");
builder.Services.AddCors(x => x.AddPolicy("BloggigFrontend", builder =>
{
    builder.WithOrigins(frontendUrl)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
}));

//Adiciona DbContext e a conex�o com o SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new System.Exception("Connection string not found");
builder.Services.AddDbContext<BloggigDbContext>(options => {
    options.UseSqlServer(connectionString);
});

//Configura autentica��o
LoadAutheticationConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())   
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    var forwardedHeaderOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    };
    forwardedHeaderOptions.KnownNetworks.Clear();
    forwardedHeaderOptions.KnownProxies.Clear();
    app.UseForwardedHeaders(forwardedHeaderOptions);
}

app.UseCors("BloggigFrontend");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void LoadAutheticationConfig()
{
    var googleClientId = builder.Configuration.GetValue<string>("Google:ClientId") ?? throw new System.Exception("Google client id not found");
    var googleClientSecret = builder.Configuration.GetValue<string>("Google:ClientSecret") ?? throw new System.Exception("Google client secret not found");
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; //Esquema que vai usar para fazer login quando o usu�rio n�o estiver logado
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
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
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