﻿using HobbyMatch.DbIntegrationTests.Infrastrucutre;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace HobbyMatch.DbIntegrationTests
{
    public class DatabaseTests : BaseIntegrationTest
    {
        private readonly MsSqlContainer _dbContainer;

        public DatabaseTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _dbContainer = factory.DbContainer;
        }

        [Fact]
        public void ConnectionStateReturnsOpen()
        {
            // Given
            using DbConnection connection = new SqlConnection(_dbContainer.GetConnectionString());

            // When
            connection.Open();

            // Then
            Assert.Equal(ConnectionState.Open, connection.State);
        }

        [Fact]
        public async Task ExecScriptReturnsSuccessful()
        {
            // Given
            const string scriptContent = "SELECT 1;";

            // When
            var execResult = await _dbContainer.ExecScriptAsync(scriptContent)
                .ConfigureAwait(true);

            // Then
            Assert.True(0L.Equals(execResult.ExitCode), execResult.Stderr);
            Assert.Empty(execResult.Stderr);
        }
    }
}
