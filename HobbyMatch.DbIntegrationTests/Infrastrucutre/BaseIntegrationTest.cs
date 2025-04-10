using HobbyMatch.Database.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace HobbyMatch.DbIntegrationTests.Infrastrucutre
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
    {
        private readonly IServiceScope _scope;
        protected readonly AppDbContext DbContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //if (DbContext.Database.GetPendingMigrations().Any())
            //{
            //    DbContext.Database.Migrate();
            //}
        }

        public Task InitializeAsync()
        {
            return DbContext.Database.EnsureCreatedAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
