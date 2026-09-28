
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
    public class CartService : ICartService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<CartDTO> CreateCartAsync(CartRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Id = UnityHelper.GenerateUlid();
            var cartEntity = _mapper.Map<Cart>(request);

            await _unitOfWork.Repository<Cart>().AddAsync(cartEntity);
            await _unitOfWork.SaveChangesAsync();

            var cartResponse = _mapper.Map<CartDTO>(cartEntity);
            return cartResponse;
        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            if (string.IsNullOrWhiteSpace(cartId))
            {
                throw new ArgumentException("Cart ID cannot be null or empty.", nameof(cartId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(cartId);
                if (cart != null)
                {
                    _unitOfWork.Repository<Cart>().Delete(cart);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<CartResponse> GetAllCartsAsync()
        {
            var listCart = await _unitOfWork.Repository<Cart>().GetAllAsync();
            if (listCart == null || !listCart.Any())
            {
                return new CartResponse();
            }

            var list = _mapper.Map<List<CartDTO>>(listCart);
            var response = new CartResponse
            {
                ListCart = list
            };
            return response;
        }

        public async Task<CartDTO> GetCartByIdAsync(string cartId)
        {
            if (string.IsNullOrWhiteSpace(cartId))
            {
                throw new ArgumentException("Cart ID cannot be null or empty.", nameof(cartId));
            }
            var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(cartId);
            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart with ID '{cartId}' not found.");
            }
            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> UpdateCartAsync(CartRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(request.Id ?? string.Empty);
                if (cart != null)
                {
                    UnityHelper.CopyProperties(request, cart);

                    await _unitOfWork.Repository<Cart>().UpdateAsync(cart);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<CartItemResponse> AddItemToCartAsync(CartItemRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var cartItemEntity = _mapper.Map<CartItem>(request);

                await _unitOfWork.Repository<CartItem>().AddAsync(cartItemEntity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<CartItemResponse>(cartItemEntity);
            });
        }

        public async Task<bool> RemoveItemFromCartAsync(string cartItemId)
        {
            if (string.IsNullOrWhiteSpace(cartItemId))
            {
                throw new ArgumentException("Cart item ID cannot be null or empty.", nameof(cartItemId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var cartItem = await _unitOfWork.Repository<CartItem>().GetByIdAsync(cartItemId);
                if (cartItem != null)
                {
                    _unitOfWork.Repository<CartItem>().Delete(cartItem);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }
    }
}
