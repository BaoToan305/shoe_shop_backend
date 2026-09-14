namespace shoe_shop_backend.Application.ResquestResponse
{
    public class RefreshResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
