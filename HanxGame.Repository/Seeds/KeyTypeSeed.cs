using HanxGame.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Repository.Seeds
{
    internal class KeyTypeSeed : IEntityTypeConfiguration<KeyTypeEntity>
    {
        public void Configure(EntityTypeBuilder<KeyTypeEntity> builder)
        {
            builder.HasData(new KeyTypeEntity { Id = 1, Name = "Steam", KeyModel = "XXXXX-XXXXX-XXXXX" },
                            new KeyTypeEntity { Id = 2, Name = "Origin", KeyModel = "XXXX-XXXX-XXXX-XXXX-XXXX" },
                            new KeyTypeEntity { Id = 3, Name = "Blizzard", KeyModel = "XXXXXXXXXXXXXXXXXXXXXXXXXX" });
        }
    }
}
