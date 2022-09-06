using HanxGame.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Repository.Configurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.KeyTypeId).IsRequired();
            builder.Property(x => x.StatusId).IsRequired();
            builder.Property(x => x.DefaultBuyingPrice).HasPrecision(18,4);
            builder.Property(x => x.DefaultSellingPrice).HasPrecision(18,4);
        }
    }
}
