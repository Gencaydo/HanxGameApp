using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string? UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
