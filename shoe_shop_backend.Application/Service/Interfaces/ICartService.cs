

using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> GetAllCartsAsync();
        Task<CartDTO> GetCartByIdAsync(string cartId);
        Task<CartDTO> CreateCartAsync(CartRequest request);
        Task<bool> UpdateCartAsync(CartRequest request);
        Task<bool> DeleteCartAsync(string cartId);

        Task<CartItemResponse> AddItemToCartAsync(CartItemRequest request);
        Task<bool> RemoveItemFromCartAsync(string cartItemId);
    }
}
