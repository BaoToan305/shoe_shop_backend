using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Domain.Main
{
    public class Order
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? AddressId { get; set; }
        public string? OrderCode { get; set; }
        public byte Status { get; set; }
        public byte PaymentMethod { get; set; }
        public byte PaymentStatus { get; set; }
        public decimal Subamount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
