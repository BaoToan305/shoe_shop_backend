using AutoMapper;
using Microsoft.Extensions.Configuration;
using shoe_shop_backend.Application.Helper;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Application.Service.Interfaces;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Domain.Main;

namespace shoe_shop_backend.Application.Service.Imp
{
    public class RoleService : IRoleService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<RoleResponse> CreateRoleAsync(RoleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var roleEntity = _mapper.Map<Role>(request);

            await _unitOfWork.Repository<Role>().AddAsync(roleEntity);
            await _unitOfWork.SaveChangesAsync();

            var roleResponse = _mapper.Map<RoleResponse>(roleEntity);
            return roleResponse;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ArgumentException("Role ID cannot be null or empty.", nameof(roleId));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var role = await _unitOfWork.Repository<Role>().GetByIdAsync(roleId);
                if (role != null)
                {
                    _unitOfWork.Repository<Role>().Delete(role);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }

        public async Task<List<RoleResponse>> GetAllRolesAsync()
        {
            var listRole = await _unitOfWork.Repository<Role>().GetAllAsync();
            if (listRole == null || !listRole.Any())
            {
                return [];
            }
            return listRole.Select(r => _mapper.Map<RoleResponse>(r)).ToList();
        }

        public async Task<RoleResponse> GetRoleByIdAsync(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new ArgumentException("Role ID cannot be null or empty.", nameof(roleId));
            }
            var role = await _unitOfWork.Repository<Role>().GetByIdAsync(roleId);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID '{roleId}' not found.");
            }
            return _mapper.Map<RoleResponse>(role);
        }

        public async Task<bool> UpdateRoleAsync(RoleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.Id ?? string.Empty);
                if (role != null)
                {
                    UnityHelper.CopyProperties(request, role);

                    await _unitOfWork.Repository<Role>().UpdateAsync(role);
                }
                await _unitOfWork.SaveChangesAsync();

                return true;
            });
        }
    }
}
