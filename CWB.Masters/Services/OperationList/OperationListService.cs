using AutoMapper;
using CWB.Logging;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.OperationList;
using CWB.Masters.ViewModels.OperationList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Services.OperationList
{
    public class OperationListService : IOperationListService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationListRepository _operationListRepository;
        private readonly IOperationalDocumentRepository _operationalDocumentRepository;

        public OperationListService(ILoggerManager logger, IMapper mapper,
            IUnitOfWork unitOfWork, IOperationListRepository operationListRepository, IOperationalDocumentRepository operationalDocumentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _operationListRepository = operationListRepository;
            _operationalDocumentRepository = operationalDocumentRepository;
        }

        public bool CheckIfOperationExisit(CheckOperationVM checkOperationVM)
        {
            var operation = _operationListRepository.GetRangeAsync(o => o.Operation == checkOperationVM.Operation &&
            o.TenantId == checkOperationVM.TenantId);
            if (!operation.Any())
            {
                return false;
            }
            return (operation.First().Id != checkOperationVM.OperationId);
        }

        public IEnumerable<OperationalDocumentListVM> GetOperationDocumentTypes(long TenantId, long OperationId)
        {
            var operationDocumentTypes = _operationalDocumentRepository.GetRangeAsync(o => o.TenantId == TenantId && o.OperationListId == OperationId);
            return _mapper.Map<IEnumerable<OperationalDocumentListVM>>(operationDocumentTypes);
        }

        public async Task<OperationalDocumentListVM> OperationDocumentTypes(OperationalDocumentListVM operationalDocumentListVM)
        {
            var operationDocument = _mapper.Map<OperationalDocument>(operationalDocumentListVM);
            if (operationDocument.Id == 0)
            {
                await _operationalDocumentRepository.AddAsync(operationDocument);
            }
            else
            {
                operationDocument = await _operationalDocumentRepository.UpdateAsync(operationDocument.Id, operationDocument);
            }
            await _unitOfWork.CommitAsync();
            operationalDocumentListVM.OperationalDocumentId = operationDocument.Id;
            return operationalDocumentListVM;
        }

        public IEnumerable<OperationListVM> GetOperationsByTenant(long tenantID)
        {
            var operationLists = _operationListRepository.GetRangeAsync(o => o.TenantId == tenantID);
            return _mapper.Map<IEnumerable<OperationListVM>>(operationLists);
        }

        public async Task<OperationListVM> Operation(OperationListVM operationVM)
        {
            var operation = _mapper.Map<Domain.OperationList>(operationVM);
            if (operation.Id == 0)
            {
                await _operationListRepository.AddAsync(operation);
            }
            else
            {
                operation = await _operationListRepository.UpdateAsync(operation.Id, operation);
            }
            await _unitOfWork.CommitAsync();
            operationVM.OperationId = operation.Id;
            return operationVM;
        }

        public OperationListVM Operation(long Id, long TenantId)
        {
            var operationLists = _operationListRepository.GetRangeAsync(o => o.TenantId == TenantId && o.Id == Id);
            if (operationLists.Any())
            {
                return _mapper.Map<OperationListVM>(operationLists.First());
            }
            return null;
        }


        public async Task<bool> DeleteOperationDoc(long opDocId, long tenantId)
        {
            var co = await _operationalDocumentRepository.SingleOrDefaultAsync(m => m.Id == opDocId && m.TenantId == tenantId);
            if (co != null)
            {
                try
                {
                    _operationalDocumentRepository.Remove(co);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                catch (Exception ex) { }
            }
            return false;
        }

    }
}
