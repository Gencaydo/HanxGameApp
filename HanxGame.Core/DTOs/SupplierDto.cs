using HanxGame.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Core.DTOs
{
    public class SupplierDto:BaseDto
    {
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int GameId { get; set; }
        public int StatusId { get; set; }
    }
}
