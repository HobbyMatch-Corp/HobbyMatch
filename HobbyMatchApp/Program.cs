using HobbyMatch.App.Auth;
using HobbyMatch.App.Auth.TokenService;
using HobbyMatch.App.Components;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddTransient<AuthHttpClientHandler>();

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddHttpClient("AuthenticatedClient")
	.AddHttpMessageHandler<AuthHttpClientHandler>();

builder.Services.AddMudServices();

var app = builder.Build();

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
