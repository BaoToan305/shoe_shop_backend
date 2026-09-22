using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoe_shop_backend.Application.DTO
{
    public class AddressDTO
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? RecipientName { get; set; }
        public string? Phone { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? DetailAddress { get; set; }
        public bool IsDefault { get; set; }
    }
}
