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
    public class MRBomGroupServices : IMRBomGroupServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMRBomGroupRepository _mRBomGroupRepository;

        public MRBomGroupServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IMRBomGroupRepository mRBomGroupRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mRBomGroupRepository = mRBomGroupRepository;
        }

        public IEnumerable<MRBomGroupVM> GetMRBomGroupsByTenant(long TenantId)
        {
            var mRBomGroups = _mRBomGroupRepository.GetRangeAsync(x => x.TenantId == TenantId);
            return _mapper.Map<IEnumerable<MRBomGroupVM>>(mRBomGroups);
        }

        public async Task AddMRBomGroup(MRBomGroupVM model)
        {
            var mRBomGroup = _mapper.Map<MRBomGroup>(model);
            await _mRBomGroupRepository.AddAsync(mRBomGroup);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateMRBomGroup(long MRBomGroupId, MRBomGroupVM model)
        {
            var mRBomGroup = _mapper.Map<MRBomGroup>(model);
            await _mRBomGroupRepository.UpdateAsync(MRBomGroupId, mRBomGroup);
            await _unitOfWork.CommitAsync();
        }
    }
}
