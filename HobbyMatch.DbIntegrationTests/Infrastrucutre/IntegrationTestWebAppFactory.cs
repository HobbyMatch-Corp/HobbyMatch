using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using HobbyMatch.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using DotNet.Testcontainers.Builders;

namespace HobbyMatch.DbIntegrationTests.Infrastrucutre
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        protected MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(_dbContainer.GetConnectionString());
                });
            });
        }
        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            var context = Services.GetRequiredService<AppDbContext>();
            await context.Database.EnsureCreatedAsync();
        }

        public new Task DisposeAsync()
        {
            return _dbContainer.DisposeAsync().AsTask();
        }
    }
}
