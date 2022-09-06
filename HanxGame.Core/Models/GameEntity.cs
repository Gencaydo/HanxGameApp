using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Core.Models
{
    public class GameEntity : BaseEntity
    {
        public int Stock { get; set; }
        public decimal DefaultBuyingPrice { get; set; }
        public decimal DefaultSellingPrice { get; set; }
        public string? ResponseUserId { get; set; }
        public int KeyTypeId { get; set; }
        public KeyTypeEntity? KeyType { get; set; }
        public int StatusId { get; set; }
        public StatusEntity? Status { get; set; }

    }
}
