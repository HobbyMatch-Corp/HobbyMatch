using System.Text;
using HobbyMatch.API.Handlers;
using HobbyMatch.BL.Configuration;
using HobbyMatch.BL.Services.Auth;
using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Database.Data;
using HobbyMatch.Database.Repositories.User;
using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));

builder.Services.AddOpenApi();
builder.Services.AddIdentity<Organizer, IdentityRole<int>>(
opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 8;
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    var jwtOptions = builder.Configuration.GetSection(JwtOptions.JwtOptionsKey).Get<JwtOptions>() ??
                     throw new ArgumentException(nameof(JwtOptions));
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true, 
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
    };
});

builder.Services.AddAuthentication();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
