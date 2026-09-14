namespace shoe_shop_backend.Domain.Main
{
    public class CartItem
    {
        public string? Id { get; set; }
        public string? CartId { get; set; }
        public string? ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
