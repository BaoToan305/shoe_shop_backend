namespace shoe_shop_backend.Application.DTO
{
    public class ProductDTO
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
    }
}
