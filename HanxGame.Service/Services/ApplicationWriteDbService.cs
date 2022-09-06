using HanxGame.Core.DTOs;
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
    public class ApplicationWriteDbService : IApplicationWriteDbService
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IApplicationWriteDb applicationWriteDb;

        public ApplicationWriteDbService(IApplicationDbContext applicationDbContext, IApplicationWriteDb applicationWriteDb)
        {
            this.applicationDbContext = applicationDbContext;
            this.applicationWriteDb = applicationWriteDb;
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await applicationWriteDb.ExecuteAsync(sql, param, transaction);
        }
    }
}
