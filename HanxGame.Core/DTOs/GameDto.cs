using HanxGame.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Core.DTOs
{
    public class GameDto : BaseDto
    {
        public int Stock { get; set; }
        public decimal DefaultBuyingPrice { get; set; }
        public decimal DefaultSellingPrice { get; set; }
        public int StatusId { get; set; }
        public int KeyTypeId { get; set; }
    }
}
