namespace shoe_shop_backend.Domain.Main
{
    public class Product
    {
        public string? Id { get; set; } 
        public string? Name { get; set; }
        public string? BrandId { get; set; }
        public string? CategoryId { get; set; } 
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public byte? Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
