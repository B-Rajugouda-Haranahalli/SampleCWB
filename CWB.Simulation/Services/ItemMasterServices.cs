using AutoMapper;
using CWB.Logging;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;
using CWB.Simulation.Repositories;
using CWB.Simulation.ViewModels;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public class ItemMasterServices : IItemMasterServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemMasterRepository _itemMasterRepository;

        public ItemMasterServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IItemMasterRepository itemMasterRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _itemMasterRepository = itemMasterRepository;
        }

        public ItemMasterVM GetItemMasterByTenant(long TenantId)
        {
            var itemMaster = _itemMasterRepository.GetRangeAsync(x => x.TenantId == TenantId);
            return _mapper.Map<ItemMasterVM>(itemMaster);
        }

        public async Task AddItemMaster(ItemMasterVM model)
        {
            var itemMaster = _mapper.Map<ItemMaster>(model);
            await _itemMasterRepository.AddAsync(itemMaster);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateItemMaster(long ItemMasterId, ItemMasterVM model)
        {
            var itemMaster = _mapper.Map<ItemMaster>(model);
            await _itemMasterRepository.UpdateAsync(ItemMasterId, itemMaster);
            await _unitOfWork.CommitAsync();
        }
    }
}
