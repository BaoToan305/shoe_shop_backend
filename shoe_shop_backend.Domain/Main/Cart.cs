namespace shoe_shop_backend.Domain.Main
{
    public class Cart
    {
        public string? Id { get; set; } 
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
