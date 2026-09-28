

using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Application.Service.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrdersAsync();
        Task<OrderResponse> GetOrderByIdAsync(string orderId);
        Task<OrderResponse> CreateOrderAsync(OrderRequest request);
        Task<bool> UpdateOrderAsync(OrderRequest request);
        Task<bool> DeleteOrderAsync(string orderId);

        Task<OrderItemResponse> AddItemToOrderAsync(OrderItemRequest request);
        Task<bool> RemoveItemFromOrderAsync(string orderItemId);
    }
}
