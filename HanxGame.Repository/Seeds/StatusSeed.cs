using HanxGame.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Repository.Seeds
{
    internal class StatusSeed : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> builder)
        {
            builder.HasData(new StatusEntity { Id = 1, Name = "Active", CreateDate = DateTime.Now },
                           new StatusEntity { Id = 2, Name = "Passive", CreateDate = DateTime.Now },
                           new StatusEntity { Id = 3, Name = "Updated", CreateDate = DateTime.Now },
                           new StatusEntity { Id = 4, Name = "Deleted", CreateDate = DateTime.Now });
        }
    }
}
