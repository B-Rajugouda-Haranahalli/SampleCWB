using CWB.App.AppUtils;
using CWB.App.Models.BusinessProcesses;
using CWB.App.Models.WorkOrder;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Services.ProductionPlanWo
{
    public class WOService:IWOService
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;

        public WOService(ILoggerManager logger,ApiUrls apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));
        }

        public async Task<IEnumerable<WOSOVM>> GetSoWoRel(long workOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/getsowo/{workOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<IEnumerable<WOSOVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<ProductionPlan_WoVM>> AllProductionPlan_Wo()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/allproductionplanwo/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<IEnumerable<ProductionPlan_WoVM>>.GetAsync(uri, headers);
        }

        public async Task<List<ProductionPlan_WoVM>> ProductionPlanWoPost(IEnumerable<ProductionPlan_WoVM> productions)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/productionplan");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in productions)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<ProductionPlan_WoVM>>.PostAsync(uri, productions, headers);
        }

        public async Task<List<ProcPlanVM>> ProcPlanPost(IEnumerable<ProcPlanVM> procPlans)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/procplan");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in procPlans)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<ProcPlanVM>>.PostAsync(uri, procPlans, headers);
        }

        public async Task<List<WorkOrdersVM>> UpdateMultipleWorkOrder(IEnumerable<WorkOrdersVM> workOrders)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/updatemultipleworkorder");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in workOrders)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<WorkOrdersVM>>.PostAsync(uri, workOrders, headers);
        }
        public async Task<List<BOMListVM>> BomListPost(IEnumerable<BOMListVM> bomlist)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/bomlistwo");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in bomlist)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<BOMListVM>>.PostAsync(uri, bomlist, headers);
        }

        public async Task<IEnumerable<ProcPlanVM>> GetAllProcPlan()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/allprocplan/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<IEnumerable<ProcPlanVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<BOMListVM>> GetAllBomlist()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/allbomlist/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<IEnumerable<BOMListVM>>.GetAsync(uri, headers);
        }

        public async Task<WOStatusVM> GetWOStatus(long Id)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/getwostatus/{Id}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<WOStatusVM>.GetAsync(uri, headers);
        }

        public async Task<List<ChildWoRelVM>> PostChildWoRel(IEnumerable<ChildWoRelVM> childWoRels)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/childworel");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in childWoRels)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<ChildWoRelVM>>.PostAsync(uri, childWoRels, headers);
        }
        public async Task<List<McTimeListVM>> PostMcTimeList(IEnumerable<McTimeListVM> mcTimeListVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/postmctimelist");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in mcTimeListVMs)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<McTimeListVM>>.PostAsync(uri, mcTimeListVMs, headers);
        }
        public async Task<IEnumerable<McTimeListVM>> GetAllMcTimeList()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/allmctimelist/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<IEnumerable<McTimeListVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<WorkOrdersVM>> AllParentChildWos(long parentWoId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/allparentchildwos/{parentWoId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<WorkOrdersVM>>.GetAsync(uri, headers);
        }
        public async Task<List<PODetailsVM>> PODetails(IEnumerable<PODetailsVM> pODetails)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/multiplepodetails");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in pODetails)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<PODetailsVM>>.PostAsync(uri, pODetails, headers);
        }
        public async Task<List<POHeaderVM>> POHeader(IEnumerable<POHeaderVM> pOHeaderVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/multiplepoheaders");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in pOHeaderVMs)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<POHeaderVM>>.PostAsync(uri, pOHeaderVMs, headers);
        }
    }
}
