
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
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Id = UnityHelper.GenerateUlid();
            var categoryEntity = _mapper.Map<Categorys>(request);

            await _unitOfWork.Repository<Categorys>().AddAsync(categoryEntity);
            await _unitOfWork.SaveChangesAsync();

            var categoryResponse = _mapper.Map<CategoryDTO>(categoryEntity);
            return categoryResponse;
        }

        public async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(categoryId));
            }
            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var categorys = await _unitOfWork.Repository<Categorys>().GetByIdAsync(categoryId);
                if (categorys != null)
                {
                    _unitOfWork.Repository<Categorys>().Delete(categorys);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<CategoryResponse> GetAllCategorysAsync()
        {
            var listCategory = await _unitOfWork.Repository<Categorys>().GetAllAsync();
            if (listCategory == null || !listCategory.Any())
            {
                return new CategoryResponse();
            }
            var list = _mapper.Map<List<CategoryDTO>>(listCategory);
            var response = new CategoryResponse
            {
                Categorys = list,
            };

            return response;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
            {
                throw new ArgumentException("Category ID cannot be null or empty.", nameof(categoryId));
            }
            var category = await _unitOfWork.Repository<Categorys>().GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID '{categoryId}' not found.");
            }
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> UpdateCategoryAsync(CategoryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var category = await _unitOfWork.Repository<Categorys>().GetByIdAsync(request.Id ?? string.Empty);
                if (category != null)
                {
                    UnityHelper.CopyProperties(request, category);

                    await _unitOfWork.Repository<Categorys>().UpdateAsync(category);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }
    }
}
