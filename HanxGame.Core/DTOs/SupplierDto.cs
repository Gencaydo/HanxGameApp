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
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Town { get; set; }
        public string? State { get; set; }
        public int PostCode { get; set; }
        public string? Country { get; set; }
        public bool IsBillingAddress { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int GameId { get; set; }
        public string? GameName { get; set; }
        public int StatusId { get; set; }
    }
}
