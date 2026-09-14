namespace shoe_shop_backend.Domain.Main
{
    public class Address
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
