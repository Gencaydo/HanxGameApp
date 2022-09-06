using HanxGame.Core.Models;
using HanxGame.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Repository
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<KeyTypeEntity> KeyTypes { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<StatusEntity> Status { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
    }
}
