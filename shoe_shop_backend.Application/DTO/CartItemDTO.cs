

namespace shoe_shop_backend.Application.DTO
{
    public class CartItemDTO
    {
        public string? Id { get; set; }
        public string? CartId { get; set; }
        public string? ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
