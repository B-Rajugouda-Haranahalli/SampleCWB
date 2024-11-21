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
    public class PlantServices : IPlantServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlantRepository _plantRepository;

        public PlantServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IPlantRepository plantRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _plantRepository = plantRepository;
        }

        public async Task AddPlant(PlantVM model)
        {
            var plant = _mapper.Map<Plant>(model);
            await _plantRepository.AddAsync(plant);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<PlantVM> GetPlants(long TenantID)
        {
            var plants = _plantRepository.GetRangeAsync(x => x.TenantId == TenantID);
            return _mapper.Map<IEnumerable<PlantVM>>(plants);
        }

        public async Task UpdatePlant(long PlantID, PlantVM model)
        {
            var plant = _mapper.Map<Plant>(model);
            await _plantRepository.UpdateAsync(PlantID, plant);
            await _unitOfWork.CommitAsync();
        }
    }
}
