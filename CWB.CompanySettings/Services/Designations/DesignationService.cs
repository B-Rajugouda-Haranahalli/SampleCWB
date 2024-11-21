using AutoMapper;
using CWB.CompanySettings.Infrastructure;
using CWB.CompanySettings.Repositories.Designations;
using CWB.CompanySettings.ViewModels.Designations;
using CWB.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Services.Designations
{
    public class DesignationService : IDesignationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDesignationRepository _designationRepository;

        public DesignationService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IDesignationRepository designationRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _designationRepository = designationRepository;

        }

        public bool CheckDesignationExisit(CheckDesignationVM checkDesignationVM)
        {
            var docTypes = _designationRepository.GetRangeAsync(d => d.Name == checkDesignationVM.Name &&
            d.TenantId == checkDesignationVM.TenantId);
            if (!docTypes.Any())
            {
                return false;
            }
            return (docTypes.First().Id != checkDesignationVM.DesignationId);
        }

        public async Task<DesignationVM> Designation(DesignationVM designationVM)
        {
            var designation = _mapper.Map<Domain.Designation>(designationVM);
            if (designation.Id == 0)
            {
                await _designationRepository.AddAsync(designation);
            }
            else
            {
                designation = await _designationRepository.UpdateAsync(designation.Id, designation);
            }
            await _unitOfWork.CommitAsync();
            designationVM.DesignationId = designation.Id;
            return designationVM;
        }

        public IEnumerable<DesignationListVM> GetDesignations(long TenantId)
        {
            var designations = _designationRepository.GetRangeAsync(d => d.TenantId == TenantId);
            return _mapper.Map<IEnumerable<DesignationListVM>>(designations);
        }

        public async Task<bool> DelDesignation(long designationId)
        {
            try
            {
                var designation = await _designationRepository.SingleOrDefaultAsync(d => d.Id == designationId);
                if (designation != null)
                {
                    if (designation.Id > 0)
                    {
                        _designationRepository.Remove(designation);
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
