

using AutoMapper;
using Microsoft.Extensions.Configuration;
using shoe_shop_backend.Application.DTO;
using shoe_shop_backend.Application.Helper;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;
using shoe_shop_backend.Domain.Exception;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Service.Imp
{
    public class BrandService : IBrandService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<BrandsDTO> CreateBrandAsync(BrandRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Id = UnityHelper.GenerateUlid();
            var brandEntity = _mapper.Map<Brands>(request);

            await _unitOfWork.Repository<Brands>().AddAsync(brandEntity);
            await _unitOfWork.SaveChangesAsync();

            var brandResponse = _mapper.Map<BrandsDTO>(brandEntity);
            return brandResponse;
        }

        public async Task<bool> DeleteBrandAsync(string brandId)
        {
            if (string.IsNullOrWhiteSpace(brandId))
            {
                throw new ArgumentException("Brand ID cannot be null or empty.", nameof(brandId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var brand = await _unitOfWork.Repository<Brands>().GetByIdAsync(brandId);
                if (brand != null)
                {
                    _unitOfWork.Repository<Brands>().Delete(brand);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<BrandResponse> GetAllBrandsAsync()
        {
            var listBrand = await _unitOfWork.Repository<Brands>().GetAllAsync();
            if (listBrand == null || !listBrand.Any())
            {
                return new BrandResponse();
            }

            var list = _mapper.Map<List<BrandsDTO>>(listBrand);
            var response = new BrandResponse
            {
                Brands = list
            };

            return response;
        }

        public async Task<BrandsDTO> GetBrandByIdAsync(string brandId)
        {
            if (string.IsNullOrWhiteSpace(brandId))
            {
                throw new ArgumentException("Brand ID cannot be null or empty.", nameof(brandId));
            }
            var brand = await _unitOfWork.Repository<Brands>().GetByIdAsync(brandId);
            if (brand == null)
            {
                throw new KeyNotFoundException($"Brand with ID '{brandId}' not found.");
            }
            return _mapper.Map<BrandsDTO>(brand);
        }

        public async Task<bool> UpdateBrandAsync(BrandRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var brand = await _unitOfWork.Repository<Brands>().GetByIdAsync(request.Id ?? string.Empty);
                if (brand != null)
                {
                    UnityHelper.CopyProperties(request, brand);

                    await _unitOfWork.Repository<Brands>().UpdateAsync(brand);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }
    }
}
