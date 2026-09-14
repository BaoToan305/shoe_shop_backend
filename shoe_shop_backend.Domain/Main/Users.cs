namespace shoe_shop_backend.Domain.Main
{
    public class Users
    {
        public string? Id { get; set; } 
        public string? FullName { get; set; } 
        public string? UserName { get; set; } 
        public string? Password { get; set; } 
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? RoleId { get; set; } 
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
