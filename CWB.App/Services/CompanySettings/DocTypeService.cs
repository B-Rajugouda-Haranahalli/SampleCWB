using CWB.App.AppUtils;
using CWB.App.Models.CoSettings;
using CWB.App.Models.Plants;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.CompanySettings
{
    public class DocTypeService : IDocTypeService
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;

        public DocTypeService(ILoggerManager logger, ApiUrls apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));
        }
        public async Task<IEnumerable<DocumentTypeVM>> GetDocTypes()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/document-types/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DocumentTypeVM>>.GetAsync(uri, headers);
        }

        public async Task<DocumentTypeVM> PostDocType(DocumentTypeVM plantVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/document-type");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            plantVM.TenantId = tenantId;
            return await RestHelper<DocumentTypeVM>.PostAsync(uri, plantVM, headers);
        }

        public async Task<bool> DelDocType(long docTypeId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/deldoctype/{docTypeId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<DocumentTypeVM> GetDocType(long docTypeId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/getdoctype/{docTypeId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<DocumentTypeVM>.GetAsync(uri, headers);
        }
    }
}
