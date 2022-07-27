﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ulmo.Core.EFContext;

namespace Ulmo.Core.Factory
{
    public class ContextFactory : IContextFactory
    {
        private readonly ConnectionSettings _connectionSettings;

        public ContextFactory(IOptions<ConnectionSettings> connectionOptions)
        {
            _connectionSettings = connectionOptions.Value;
        }

        public IDatabaseContext DbContext => new DatabaseContext(GetDataContext().Options);

        private DbContextOptionsBuilder<DatabaseContext> GetDataContext()
        {
            ValidateDefaultConnection();

            var sqlConnectionBuilder = new SqlConnectionStringBuilder(_connectionSettings.DefaultConnection);

            var contextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            contextOptionsBuilder.UseSqlServer(sqlConnectionBuilder.ConnectionString);

            return contextOptionsBuilder;
        }

        private void ValidateDefaultConnection()
        {
            if (string.IsNullOrEmpty(_connectionSettings.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(_connectionSettings.DefaultConnection));
            }
        }
    }
}
