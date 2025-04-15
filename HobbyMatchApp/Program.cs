using HobbyMatch.App.Auth;
using HobbyMatch.App.Auth.CustomAuthStateProvider;
using HobbyMatch.App.Auth.TokenService;
using HobbyMatch.App.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;
using Microsoft.Extensions.Options;
using HobbyMatch.App.Services.Api;
using Microsoft.AspNetCore.Components.Authorization;
using HobbyMatch.App.Services;
using HobbyMatch.App.Services.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddTransient<AuthHttpClientHandler>();

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<EndpointProvider>();
builder.Services.AddScoped<IOrganizerApiService, OrganizerApiService>();

builder.Services.AddHttpClient("AuthenticatedClient").AddHttpMessageHandler<AuthHttpClientHandler>();
builder.Services.AddHttpClient("AuthClient", client =>
{
    var baseUrl = builder.Configuration.GetSection("ApiSettings")["BaseUrl"];
	  client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddScoped<CustomAuthStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
	provider.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddScoped<IEventApiService, EventApiService>();
builder.Services.AddScoped<EndpointProvider, EndpointProvider>();
builder.Services.AddMudServices();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var settings = scope.ServiceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;

Console.WriteLine($"HERE : {settings.BaseUrl}");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
