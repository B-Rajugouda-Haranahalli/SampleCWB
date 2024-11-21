using AutoMapper;
using CWB.Logging;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;
using CWB.Simulation.Repositories;
using CWB.Simulation.SimulationUtils;
using CWB.Simulation.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public class VendorServices : IVendorServices
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVendorRepository _vendorRepository;

        public VendorServices(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IVendorRepository vendorRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _vendorRepository = vendorRepository;
        }
        public async Task AddVendor(VendorVM model)
        {
            var vendor = _mapper.Map<Vendor>(model);
            await _vendorRepository.AddAsync(vendor);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<VendorVM> GetVendor(long TenantID)
        {
            var vendors = _vendorRepository.GetRangeAsync(x => x.TenantId == TenantID);
            return _mapper.Map<IEnumerable<VendorVM>>(vendors);
        }

        public IEnumerable<VendorVM> GetVendorByType(string Type)
        {
            VendorType vendorType;
            if (Enum.TryParse(Type, out vendorType))
            {
                var vendors = _vendorRepository.GetRangeAsync(x => x.Type == vendorType);
                return _mapper.Map<IEnumerable<VendorVM>>(vendors);
            }
            else
            {
                _logger.LogError($"Invalid Status: {Type}");
                throw new Exception($"Invalid Status: {Type}");
            }
        }

        public async Task UpdateVendor(long VendorId, VendorVM model)
        {
            var vendor = _mapper.Map<Vendor>(model);
            await _vendorRepository.UpdateAsync(VendorId, vendor);
            await _unitOfWork.CommitAsync();
        }
    }
}
