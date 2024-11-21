using CWB.App.AppUtils;
using CWB.App.Models.OperationList;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Services.Masters
{
    public class OperationService : IOperationService
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;

        public OperationService(ILoggerManager logger, IOptions<ApiUrls> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));
        }

        public async Task<bool> CheckIfOperationExisit(long OperationId, string Operation)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/check-operation");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            var data = new { OperationId, Operation, TenantId = tenantId };
            return await RestHelper<bool>.PostAsync(uri, data, headers);
        }

        public async Task<IEnumerable<DocumentTypeListVM>> GetOperationDocTypes(long OperationId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/document-types/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            var docType = await RestHelper<List<DocumentTypeListVM>>.GetAsync(uri, headers);
            var operationalDocTypes = await GetOperationalDocTypesByOptId(OperationId);
            return docType.Where(d => !operationalDocTypes.Any(o => o.DocumentTypeId == d.DocumentTypeId));
        }

        public async Task<IEnumerable<OperationalListDocmentsVM>> GetOperationDocTypesList(long OperationId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/document-types/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            var docType = await RestHelper<List<DocumentTypeListVM>>.GetAsync(uri, headers);
            var operationalDocTypes = await GetOperationalDocTypesByOptId(OperationId);

            return from d in docType
                   join o in operationalDocTypes on d.DocumentTypeId equals o.DocumentTypeId
                   select new OperationalListDocmentsVM
                   {
                       DocumentType = d.Name,
                       DocumentTypeId = d.DocumentTypeId,
                       IsMandatory = o.IsMandatory,
                       OperationalDocumentId = o.OperationalDocumentId,
                       OperationListId = o.OperationListId
                   };
        }

        public async Task<IEnumerable<OperationListVM>> GetOperationsList()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/operation-list/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<OperationListVM>>.GetAsync(uri, headers);
        }

        public async Task<OperationListVM> Operation(OperationListVM operationVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/operation");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            operationVM.TenantId = tenantId;
            return await RestHelper<OperationListVM>.PostAsync(uri, operationVM, headers);
        }

        public async Task<OperationListVM> Operation(long Id)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/operation-list/{Id}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<OperationListVM>.GetAsync(uri, headers);
        }

        public async Task<OperationDocumentTypeVM> OperationDocument(OperationDocumentTypeVM operationDocumentTypeVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/operation-doctype");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            var data = new OperationalDocumentListVM
            {
                OperationalDocumentId = operationDocumentTypeVM.OperationListDocumentId,
                DocumentTypeId = operationDocumentTypeVM.OperationDocumentTypeId,
                IsMandatory = operationDocumentTypeVM.IsOperationDocumentMandatory,
                TenantId = tenantId,
                OperationListId = operationDocumentTypeVM.OperationListIdForDocType
            };
            var result = await RestHelper<OperationalDocumentListVM>.PostAsync(uri, data, headers);
            operationDocumentTypeVM.OperationDocumentTypeId = result.OperationalDocumentId;
            return operationDocumentTypeVM;
        }

        public async Task<IEnumerable<OperationalDocumentListVM>> GetOperationalDocTypesByOptId(long Id)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/operation-list-doctypes/{Id}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<OperationalDocumentListVM>>.GetAsync(uri, headers);
        }
        public async Task<bool> DeletOperationDoc(long opDocId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deleteopdoclist/{opDocId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
    }
}
