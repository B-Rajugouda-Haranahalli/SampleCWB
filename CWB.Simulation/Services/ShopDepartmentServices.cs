using AutoMapper;
using CWB.Logging;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;
using CWB.Simulation.Repositories;
using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public class ShopDepartmentServices : IShopDepartmentServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShopDepartmentRepository _shopDepartmentRepository;

        public ShopDepartmentServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IShopDepartmentRepository shopDepartmentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _shopDepartmentRepository = shopDepartmentRepository;
        }

        public async Task AddShopDepartment(ShopDepartmentVM model)
        {
            var shopDepartment = _mapper.Map<ShopDepartment>(model);
            await _shopDepartmentRepository.AddAsync(shopDepartment);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<ShopDepartmentVM> GetShopDepartmentByPlant(long TenantID, long PlantID)
        {
            var model = _shopDepartmentRepository.GetRangeAsync(x => x.TenantId == TenantID && x.PlantId == PlantID);
            return _mapper.Map<IEnumerable<ShopDepartmentVM>>(model);
        }

        public IEnumerable<ShopDepartmentVM> GetShopDepartmentByTenant(long TenantID)
        {
            var model = _shopDepartmentRepository.GetRangeAsync(x => x.TenantId == TenantID);
            return _mapper.Map<IEnumerable<ShopDepartmentVM>>(model);
        }

        public async Task UpdateShopDepartment(long ShopDepartmentID, ShopDepartmentVM model)
        {
            var shopDepartment = _mapper.Map<ShopDepartment>(model);
            await _shopDepartmentRepository.UpdateAsync(ShopDepartmentID, shopDepartment);
            await _unitOfWork.CommitAsync();
        }
    }
}
