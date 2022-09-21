using Dapper;
using HanxGame.Core.Repositories;
using HanxGame.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Service.Services
{
    public class ApplicationExecuteQueryDbService : IApplicationExecuteQueryDbService
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IApplicationExecuteQueryDb applicationExecuteQueryDb;

        public ApplicationExecuteQueryDbService(IApplicationDbContext applicationDbContext, IApplicationExecuteQueryDb applicationReadDb)
        {
            this.applicationDbContext = applicationDbContext;
            this.applicationExecuteQueryDb = applicationReadDb;
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await applicationExecuteQueryDb.QueryAsync<T>(sql, param, transaction)).AsList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await applicationExecuteQueryDb.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await applicationExecuteQueryDb.QuerySingleAsync<T>(sql, param, transaction);
        }
    }
}
