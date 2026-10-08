using AutoMapper;
using Microsoft.Extensions.Configuration;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.Helper;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<ProductDTO> CreateProductAsync(ProductRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Id = UnityHelper.GenerateUlid();
            var productEntity = _mapper.Map<Product>(request);
            
            await _unitOfWork.Repository<Product>().AddAsync(productEntity);
            await _unitOfWork.SaveChangesAsync();
           
            var productResponse = _mapper.Map<ProductDTO>(productEntity);
            return productResponse;
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
           if(string.IsNullOrWhiteSpace(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }
           
           return await _unitOfWork.ExecuteTransactionAsync(async () =>
           {
               var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
               if (product != null)
               {
                   _unitOfWork.Repository<Product>().Delete(product);
               }
               await _unitOfWork.SaveChangesAsync();

               return true;
           });
        }

        public async Task<ProductResponse> GetAllProductsAsync()
        {
           var listProduct = await _unitOfWork.Repository<Product>().GetAllAsync();
           if(listProduct == null || !listProduct.Any())
            {
                return new ProductResponse();
            }

            var list = _mapper.Map<List<ProductDTO>>(listProduct);
            var response = new ProductResponse
            {
                Products = list
            };

            return response;
        }

        public async Task<ProductDTO> GetProductByIdAsync(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID '{productId}' not found.");
            }
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProductAsync(ProductRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id ?? string.Empty);
                if (product != null)
                {
                    UnityHelper.CopyProperties(request, product);

                    await _unitOfWork.Repository<Product>().UpdateAsync(product);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
         }
    }
}
