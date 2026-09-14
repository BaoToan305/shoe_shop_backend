using System;
namespace shoe_shop_backend.Application.ResquestResponse
{
    public class LoginRespponse
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleId { get; set; }
        public int IsActive { get; set; }

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
