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
    public class MachineTypeServices : IMachineTypeServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMachineTypeRepository _machineTypeRepository;
        public MachineTypeServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IMachineTypeRepository machineTypeRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _machineTypeRepository = machineTypeRepository;
        }

        public async Task AddMachineType(MachineTypeVM model)
        {
            var machineType = _mapper.Map<MachineType>(model);
            await _machineTypeRepository.AddAsync(machineType);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<MachineTypeVM> GetMachineTypes(long TenantID)
        {
            var model = _machineTypeRepository.GetRangeAsync(x => x.TenantId == TenantID);
            return _mapper.Map<IEnumerable<MachineTypeVM>>(model);
        }

        public async Task UpdateMachineType(long MachineTypeID, MachineTypeVM model)
        {
            var machineType = _mapper.Map<MachineType>(model);
            await _machineTypeRepository.UpdateAsync(MachineTypeID, machineType);
            await _unitOfWork.CommitAsync();
        }
    }
}
