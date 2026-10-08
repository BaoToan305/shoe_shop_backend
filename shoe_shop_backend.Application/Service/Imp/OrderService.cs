using AutoMapper;
using Microsoft.Extensions.Configuration;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.Helper;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Service.Imp
{
    public class OrderService : IOrderService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Id = UnityHelper.GenerateUlid();
            var orderEntity = _mapper.Map<Order>(request);

            await _unitOfWork.Repository<Order>().AddAsync(orderEntity);
            await _unitOfWork.SaveChangesAsync();

            var orderResponse = _mapper.Map<OrderResponse>(orderEntity);
            return orderResponse;
        }

        public async Task<bool> DeleteOrderAsync(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
                if (order != null)
                {
                    _unitOfWork.Repository<Order>().Delete(order);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<OrderResponse> GetAllOrdersAsync()
        {
            var listOrder = await _unitOfWork.Repository<Order>().GetAllAsync();
            if (listOrder == null || !listOrder.Any())
            {
                return new OrderResponse();
            }

            var list = _mapper.Map<List<OrderDTO>>(listOrder);
            var orderResponse = new OrderResponse
            {
                ListOrder = list
            };

            return orderResponse;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID '{orderId}' not found.");
            }
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<bool> UpdateOrderAsync(OrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.Id ?? string.Empty);
                if (order != null)
                {
                    UnityHelper.CopyProperties(request, order);

                    await _unitOfWork.Repository<Order>().UpdateAsync(order);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<OrderItemResponse> AddItemToOrderAsync(OrderItemRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var orderItemEntity = _mapper.Map<OrderItem>(request);

                await _unitOfWork.Repository<OrderItem>().AddAsync(orderItemEntity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<OrderItemResponse>(orderItemEntity);
            });
        }

        public async Task<bool> RemoveItemFromOrderAsync(string orderItemId)
        {
            if (string.IsNullOrWhiteSpace(orderItemId))
            {
                throw new ArgumentException("Order item ID cannot be null or empty.", nameof(orderItemId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var orderItem = await _unitOfWork.Repository<OrderItem>().GetByIdAsync(orderItemId);
                if (orderItem != null)
                {
                    _unitOfWork.Repository<OrderItem>().Delete(orderItem);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }
    }
}
