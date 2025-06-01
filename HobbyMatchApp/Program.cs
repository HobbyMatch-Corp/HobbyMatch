using HobbyMatch.App.Auth;
using HobbyMatch.App.Auth.CustomAuthStateProvider;
using HobbyMatch.App.Auth.TokenService;
using HobbyMatch.App.Components;
using HobbyMatch.App.Services;
using HobbyMatch.App.Services.Api;
using HobbyMatch.App.Services.Comments;
using HobbyMatch.App.Services.Events;
using HobbyMatch.App.Services.Hobbies;
using HobbyMatch.App.Services.Venues;
using HobbyMatch.BL.Services.Validation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddTransient<AuthHttpClientHandler>();

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddSingleton<TokenStore>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder
    .Services.AddHttpClient(
        "AuthenticatedClient",
        client =>
        {
            var baseUrl = builder.Configuration.GetSection("ApiSettings")["BaseUrl"];
            client.BaseAddress = new Uri(baseUrl!);
        }
    )
    .AddHttpMessageHandler<AuthHttpClientHandler>();

builder.Services.AddHttpClient(
    "AuthClient",
    client =>
    {
        var baseUrl = builder.Configuration.GetSection("ApiSettings")["BaseUrl"];
        client.BaseAddress = new Uri(baseUrl!);
    }
);
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthStateProvider>()
);
builder.Services.AddScoped<EndpointProvider>();
builder.Services.AddScoped<HttpClientUtils>();
builder.Services.AddScoped<IOrganizerApiService, OrganizerApiService>();
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddScoped<IEventApiService, EventApiService>();
builder.Services.AddScoped<IVenueApiService, VenueApiService>();
builder.Services.AddScoped<IHobbyApiService, HobbyApiService>();
builder.Services.AddScoped<ICommentApiService, CommentApiService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddMudServices();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var settings = scope.ServiceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
