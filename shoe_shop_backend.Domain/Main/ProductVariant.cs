namespace shoe_shop_backend.Domain.Main
{
    public class ProductVariant
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? Sku { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public decimal PriceModifier { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
