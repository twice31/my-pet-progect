using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BookExchange.Infrastructure
{
    public sealed class PostgreSqlConnectionOptions
    {
        public required string HostName { get; init; }
        public required string DatabaseName { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }

        public string BuildConnectionString()
        {
            return $"Host={HostName};Database={DatabaseName};Username={UserName};Password={Password}";
        }
    }
}