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
    public class MachineServices : IMachineServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMachineRepository _machineRepository;
        public MachineServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IMachineRepository machineRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _machineRepository = machineRepository;
        }

        public async Task AddMachine(MachineVM model)
        {
            var machine = _mapper.Map<Machine>(model);
            await _machineRepository.AddAsync(machine);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<MachineVM> GetMachines(long TenantID)
        {
            var machines = _machineRepository.GetRangeAsync(x => x.TenantId == TenantID);
            return _mapper.Map<IEnumerable<MachineVM>>(machines);
        }

        public async Task UpdateMachine(long MachineID, MachineVM model)
        {
            var machine = _mapper.Map<Machine>(model);
            await _machineRepository.UpdateAsync(MachineID, machine);
            await _unitOfWork.CommitAsync();
        }
    }
}
