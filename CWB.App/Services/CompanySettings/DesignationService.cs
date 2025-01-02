using CWB.App.AppUtils;
using CWB.App.Models.Designation;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.CompanySettings
{
    public class DesignationService : IDesignationService
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;

        public DesignationService(ILoggerManager logger, ApiUrls apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));
        }
        public async Task<IEnumerable<DesignationVM>> GetDesignations()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/designations/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DesignationVM>>.GetAsync(uri, headers);
        }

        public async Task<bool> CheckIfDesignationExisit(long DesignationId, string DesignationName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/check-designation");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            var data = new { DesignationId, DesignationName, TenantId = tenantId };
            return await RestHelper<bool>.PostAsync(uri, data, headers);
        }

        public async Task<DesignationVM> Designation(DesignationVM designationVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/designation");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            designationVM.TenantId = tenantId;
            return await RestHelper<DesignationVM>.PostAsync(uri, designationVM, headers);
        }
        public async Task<bool> DelDesignation(long designationId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/deldesignation/{designationId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
    }
}
