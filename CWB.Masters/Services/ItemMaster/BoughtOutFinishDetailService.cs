using AutoMapper;
using CWB.Logging;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using CWB.Masters.Repositories.ItemMaster;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Services.ItemMaster
{
    public class BoughtOutFinishDetailService : IBoughtOutFinishDetailService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBoughtOutFinishDetailRepository _boughtOutFinishDetailRepository;
        private readonly IMasterPartRepository  _masterPartRepository;
        private readonly IPartStatusChangeLogRepository _partStatusChangeLogRepository;


        public BoughtOutFinishDetailService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork,
            IBoughtOutFinishDetailRepository boughtOutFinishDetailRepository,
            IMasterPartRepository masterPartRepository, IPartStatusChangeLogRepository partStatusChangeLogRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _boughtOutFinishDetailRepository = boughtOutFinishDetailRepository;
            _masterPartRepository = masterPartRepository;
            _partStatusChangeLogRepository = partStatusChangeLogRepository;
        }
        public IEnumerable<BoughtOutFinishDetailVM> GetBoughtOutFinishDetailsByTenant(long tenantID)
        {
            var boughtoutfinishdetails = _boughtOutFinishDetailRepository.GetRangeAsync(m => m.TenantId == tenantID);         
            return _mapper.Map<IEnumerable<BoughtOutFinishDetailVM>>(boughtoutfinishdetails);
        }

        private int GetPartId(string partNo)
        {
            var rms = _masterPartRepository.GetRangeAsync(c => c.PartNo.Equals(partNo));
            if (!rms.Any())
            {
                return 0;
            }
            MasterPart part = _mapper.Map<Domain.ItemMaster.MasterPart>(rms.First());
            return (int)part.Id;
        }

        public async Task<BoughtOutFinishDetailVM> BoughtOutFinishDetail(BoughtOutFinishDetailVM boughtOutFinishDetailVM)
        {
            try { 
                var masterPart = _mapper.Map<Domain.ItemMaster.MasterPart>(boughtOutFinishDetailVM);
                var boughtoutfinishdetail = _mapper.Map<BoughtOutFinishDetail>(boughtOutFinishDetailVM);
                int id = GetPartId(masterPart.PartNo);
                boughtoutfinishdetail.PartId = id;
                boughtOutFinishDetailVM.PartId = id;
                if (id == 0)
                {
                    masterPart.Id = 0;
                    await _masterPartRepository.AddAsync(masterPart);
                    await _unitOfWork.CommitAsync();
                    boughtoutfinishdetail.PartId = (int)masterPart.Id;
                    boughtOutFinishDetailVM.PartId = (int)masterPart.Id;
                    PartStatusChangeLog partStatus = new PartStatusChangeLog()
                    {
                        MasterPartId = masterPart.Id,
                        Status = masterPart.Status,
                        ChangeReason = masterPart.StatusChangeReason,
                        TenantId = masterPart.TenantId
                    };
                    await _partStatusChangeLogRepository.AddAsync(partStatus);

                }
                else
                {
                    if (id == boughtoutfinishdetail.PartId)
                    {
                        PartStatusChangeLog partStatus = new PartStatusChangeLog()
                        {
                            MasterPartId = masterPart.Id,
                            Status = masterPart.Status,
                            ChangeReason = masterPart.StatusChangeReason,
                            TenantId = masterPart.TenantId
                        };
                        await _partStatusChangeLogRepository.AddAsync(partStatus);
                        masterPart = await _masterPartRepository.UpdateAsync(masterPart.Id, masterPart);
                        await _unitOfWork.CommitAsync();
                    }
                }
            
                if (boughtoutfinishdetail.Id == 0)
                {
                    await _boughtOutFinishDetailRepository.AddAsync(boughtoutfinishdetail);
                }
                else
                {
                    boughtoutfinishdetail = await _boughtOutFinishDetailRepository.UpdateAsync(boughtoutfinishdetail.Id, boughtoutfinishdetail);
                }
                await _unitOfWork.CommitAsync();
                boughtOutFinishDetailVM.BoughtOutFinishDetailId = boughtoutfinishdetail.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                throw ex;
            }
            return boughtOutFinishDetailVM;
        }
        public bool CheckPartNo(long partId)
        {
            var bofs = _boughtOutFinishDetailRepository.GetRangeAsync(c => c.PartId == (partId));
            if (!bofs.Any())
            {
                return false;
            }
            return true;
        }

        public async Task<BoughtOutFinishDetailVM> GetPart(int partId, long tenantId)
        {
            try
            {
                var part = _boughtOutFinishDetailRepository.GetRangeAsync(m => m.PartId == partId && m.TenantId == tenantId).OrderByDescending(m=>m.Id).FirstOrDefault();
                if (part != null)
                {
                    return _mapper.Map<BoughtOutFinishDetailVM>(part);
                }
            } catch(Exception ex)
            {
                var msg = ex.InnerException.Message;
                var src = ex.InnerException.Source;
            }
            return new BoughtOutFinishDetailVM { BoughtOutFinishDetailId = -1 };
        }
    }
}
