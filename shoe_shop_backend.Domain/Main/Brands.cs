namespace shoe_shop_backend.Domain.Main
{
    public class Brands
    {
        public string? Id { get; set; } 
        public string? Name { get; set; } 
        public string? LogoUrl { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
