
using AutoMapper;
using Microsoft.Extensions.Configuration;
using shoe_shop_backend.Application.Helper;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Service.Imp
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductVariantService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<ProductVariantResponse> CreateProductVariantAsync(ProductVariantRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var productVariantEntity = _mapper.Map<ProductVariant>(request);

            await _unitOfWork.Repository<ProductVariant>().AddAsync(productVariantEntity);
            await _unitOfWork.SaveChangesAsync();

            var productResponse = _mapper.Map<ProductVariantResponse>(productVariantEntity);
            return productResponse;
        }

        public async Task<bool> DeleteProductVariantAsync(string productVariantId)
        {
            if (string.IsNullOrWhiteSpace(productVariantId))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(productVariantId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var productVariant = await _unitOfWork.Repository<ProductVariant>().GetByIdAsync(productVariantId);
                if (productVariant != null)
                {
                    _unitOfWork.Repository<ProductVariant>().Delete(productVariant);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<List<ProductVariantResponse>> GetAllProductVariantsAsync()
        {
            var listProduct = await _unitOfWork.Repository<ProductVariant>().GetAllAsync();
            if (listProduct == null || !listProduct.Any())
            {
                return [];
            }
            return [.. listProduct.Select(p => _mapper.Map<ProductVariantResponse>(p))];
        }

        public async Task<ProductVariantResponse> GetProductVariantByIdAsync(string productVariantId)
        {
            if (string.IsNullOrWhiteSpace(productVariantId))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(productVariantId));
            }
            var productVariant = await _unitOfWork.Repository<Product>().GetByIdAsync(productVariantId);
            if (productVariant == null)
            {
                throw new KeyNotFoundException($"ID '{productVariantId}' not found.");
            }
            return _mapper.Map<ProductVariantResponse>(productVariant);
        }

        public async Task<bool> UpdateProductVariantAsync(ProductVariantRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var productVariant = await _unitOfWork.Repository<ProductVariant>().GetByIdAsync(request.Id ?? string.Empty);
                if (productVariant != null)
                {
                    UnityHelper.CopyProperties(request, productVariant);

                    await _unitOfWork.Repository<ProductVariant>().UpdateAsync(productVariant);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }
    }
}
