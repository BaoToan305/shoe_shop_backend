

namespace shoe_shop_backend.Application.DTO
{
    public class OrderItemDTO
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? ProductVariantId { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
