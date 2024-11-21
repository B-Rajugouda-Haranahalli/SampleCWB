using AutoMapper;
using CWB.Logging;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;
using CWB.Simulation.Repositories;
using CWB.Simulation.ViewModels;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public class WorkDayMasterServices : IWorkDayMasterServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkDayMasterRepository _workDayMasterRepository;
        public WorkDayMasterServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IWorkDayMasterRepository workDayMasterRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _workDayMasterRepository = workDayMasterRepository;
        }
        public async Task<WorkDayMasterVM> GetWorkDayMaster(long tenantID)
        {
            var workDayMaster = await _workDayMasterRepository.SingleOrDefaultAsync(x => x.TenantId == tenantID);
            return _mapper.Map<WorkDayMasterVM>(workDayMaster);
        }
        public async Task AddWorkDayMaster(WorkDayMasterVM WorkDayMasterVM)
        {
            var model = _mapper.Map<WorkDayMaster>(WorkDayMasterVM);
            await _workDayMasterRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateWorkDayMaster(long WorkDayMasterID, WorkDayMasterVM WorkDayMasterVM)
        {
            var model = _mapper.Map<WorkDayMaster>(WorkDayMasterVM);
            await _workDayMasterRepository.UpdateAsync(WorkDayMasterID, model);
            await _unitOfWork.CommitAsync();
        }
    }
}
