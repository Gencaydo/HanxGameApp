using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Core.Models
{
    public class SupplierEntity:BaseEntity
    {
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int GameId { get; set; }
        public GameEntity? Game { get; set; }
        public int StatusId { get; set; }
        public StatusEntity? Status { get; set; }
    }
}
