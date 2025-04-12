using HobbyMatch.BL.Services.Auth.Account;
using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly AppDbContext DbContext;
        protected readonly UserManager<Organizer> UserManager;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
            UserManager = _scope.ServiceProvider.GetRequiredService<UserManager<Organizer>>();
        }
    }
}
