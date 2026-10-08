

using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(string orderId);
        Task<OrderDTO> CreateOrderAsync(OrderRequest request);
        Task<bool> UpdateOrderAsync(OrderRequest request);
        Task<bool> DeleteOrderAsync(string orderId);

        Task<OrderItemResponse> AddItemToOrderAsync(OrderItemRequest request);
        Task<bool> RemoveItemFromOrderAsync(string orderItemId);
    }
}
