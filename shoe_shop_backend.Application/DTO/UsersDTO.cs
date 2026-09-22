

namespace shoe_shop_backend.Application.DTO
{
    public class UsersDTO
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
