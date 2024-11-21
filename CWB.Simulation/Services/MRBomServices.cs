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
    public class MRBomServices : IMRBomServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMRBomRepository _mRBomRepository;

        public MRBomServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IMRBomRepository mRBomRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mRBomRepository = mRBomRepository;
        }

        public IEnumerable<MRBomVM> GetMRBomsByTenant(long TenantId)
        {
            var mRBoms = _mRBomRepository.GetRangeAsync(x => x.TenantId == TenantId);
            return _mapper.Map<IEnumerable<MRBomVM>>(mRBoms);
        }

        public MRBomVM GetMRBomsById(long Id)
        {
            var mRBoms = _mRBomRepository.GetRangeAsync(x => x.Id == Id);
            return _mapper.Map<MRBomVM>(mRBoms);
        }

        public async Task AddMRBom(MRBomVM model)
        {
            var mRBom = _mapper.Map<MRBom>(model);
            await _mRBomRepository.AddAsync(mRBom);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateMRBom(long MRBomId, MRBomVM model)
        {
            var mRBom = _mapper.Map<MRBom>(model);
            await _mRBomRepository.UpdateAsync(MRBomId, mRBom);
            await _unitOfWork.CommitAsync();
        }
    }
}
