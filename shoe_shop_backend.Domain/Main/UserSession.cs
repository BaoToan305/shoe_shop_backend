namespace shoe_shop_backend.Domain.Main
{
    public class UserSession
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? TokenHash { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? ReplacedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
