using System.Text;
using HobbyMatch.API.Handlers;
using HobbyMatch.BL.Configuration;
using HobbyMatch.BL.Services.AppUsers;
using HobbyMatch.BL.Services.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.BL.Services.Auth.Tokens;
using HobbyMatch.BL.Services.BusinessClients;
using HobbyMatch.BL.Services.Comments;
using HobbyMatch.BL.Services.Events;
using HobbyMatch.BL.Services.Hobbies;
using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Database.Data;
using HobbyMatch.Database.Repositories.AppUsers;
using HobbyMatch.Database.Repositories.BusinessClients;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Database.Repositories.Hobbies;
using HobbyMatch.Database.Repositories.Users;
using HobbyMatch.Database.Repositories.Venues;
using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));

builder.Services.AddOpenApi();
builder
    .Services.AddIdentity<Organizer, IdentityRole<int>>(opt =>
    {
        opt.Password.RequireDigit = true;
        opt.Password.RequiredLength = 8;
        opt.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Auth
builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IBusinessClientRepository, BusinessClientRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// App users
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IEventService, EventService>();

// Buisness clietns
builder.Services.AddScoped<IBusinessClientRepository, BusinessClientRepository>();
builder.Services.AddScoped<IBusinessClientService, BusinessClientService>();

// Events
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

// Hobbies
builder.Services.AddScoped<IHobbyRepository, HobbyRepository>();
builder.Services.AddScoped<IHobbyService, HobbyService>();

//Comments
builder.Services.AddScoped<ICommentService, CommentService>();

builder
    .Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        var jwtOptions =
            builder.Configuration.GetSection(JwtOptions.JwtOptionsKey).Get<JwtOptions>()
            ?? throw new ArgumentException(nameof(JwtOptions));
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
        };
    });

builder.Services.AddAuthentication();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var app = builder.Build();

await using (var serviceScope = app.Services.CreateAsyncScope())
await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

    if (pendingMigrations.Any())
    {
        await dbContext.Database.MigrateAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Servers = Array.Empty<ScalarServer>();
    });
}

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
