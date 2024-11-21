using AutoMapper;
using CWB.Logging;
using CWB.Modules.Domain;
using CWB.Modules.Infrastructure;
using CWB.Modules.Repositories;
using CWB.Modules.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Modules.Services
{
    public class ModuleServices : IModuleServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModuleTypeRepository _moduleTypeRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleTenantConfigRepository _moduleTenantConfigRepository;
        public ModuleServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IModuleTypeRepository moduleTypeRepository, IModuleRepository moduleRepository, IModuleTenantConfigRepository moduleTenantConfigRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _moduleTypeRepository = moduleTypeRepository;
            _moduleRepository = moduleRepository;
            _moduleTenantConfigRepository = moduleTenantConfigRepository;
        }

        public async Task<IEnumerable<ModuleTypesVM>> GetAllModuleWithTypes(bool onlyActive)
        {
            var modules = await _moduleTypeRepository.GetAllModulesByTypesAsync(onlyActive);
            return _mapper.Map<IEnumerable<ModuleTypesVM>>(modules);
        }

        public async Task<IEnumerable<ModulesVM>> GetAllModulesWithTypesByTenant(long tenantID)
        {
            var moduleTenantConfig = await _moduleTenantConfigRepository.GetAllModulesByTenantConfig(tenantID);
            return _mapper.Map<IEnumerable<ModulesVM>>(moduleTenantConfig.Select(m => m.Module));
        }

        public async Task<ModulesVM> GetModule(long moduleID)
        {
            var module = await _moduleRepository.GetByIdAsync(moduleID);
            return _mapper.Map<ModulesVM>(module);
        }

        public async Task AddModule(ModulesVM modulesVM)
        {
            var module = _mapper.Map<Module>(modulesVM);
            await _moduleRepository.AddAsync(module);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateModule(long moduleID, ModulesVM modulesVM)
        {
            var module = _mapper.Map<Module>(modulesVM);
            await _moduleRepository.UpdateAsync(moduleID, module);
            await _unitOfWork.CommitAsync();
        }

        public bool CheckDuplicateModuleByName(string moduleName)
        {
            return _moduleRepository.GetRangeAsync(m => m.Name == moduleName).Any();
        }
        public bool CheckDuplicateModuleByKey(string moduleKey)
        {
            return _moduleRepository.GetRangeAsync(m => m.Name == moduleKey).Any();
        }

        public void EnableorDisableModule(ModuleTenantConfigVM moduleTenantConfigVM)
        {
            if (moduleTenantConfigVM.IsEnable)
            {
                var moduleTenantConfig = _mapper.Map<ModuleTenantConfig>(moduleTenantConfigVM);
                if (!_moduleTenantConfigRepository.GetRangeAsync(t => t.ModuleId == moduleTenantConfigVM.ModuleID && t.TenantId == moduleTenantConfigVM.TenantID).Any())
                {
                    _moduleTenantConfigRepository.AddAsync(moduleTenantConfig);
                    _unitOfWork.CommitAsync();
                }
            }
            else
            {
                var moduleTenantConfig = _moduleTenantConfigRepository.GetRangeAsync(t => t.ModuleId == moduleTenantConfigVM.ModuleID && t.TenantId == moduleTenantConfigVM.TenantID);
                _moduleTenantConfigRepository.RemoveRange(moduleTenantConfig);
                _unitOfWork.CommitAsync();
            }
        }

    }
}
