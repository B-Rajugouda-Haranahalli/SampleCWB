using CWB.App.AppUtils;
using CWB.App.Models.BusinessProcesses;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
    * /**
    *    public const string GetPOLogs = Base + "/getpologs/{tenantId}";
           public const string GetSalesOrders = Base + "/getsalesorders/{tenantId}/{customerOderId}";
           public const string GetCustomerOrders = Base + "/getcustomerorders/{tenantId}";
           public const string GetSchedules = Base + "/getdeliveryschedules/{tenantId}/{customerOrderId}";
           public const string HelloWorld = Base + "/helloworld/{tenantId}";

           public const string PostSalesOrder = Base + "/salesorder";
           public const string PostCustomerOrder = Base + "/custorder";
           public const string PostDeliverySchedule = Base + "/deliveryschedule";
           public const string PostOrderStatus = Base + "/orderstatus";
           public const string PostPOLog = Base + "/polog";
    */

namespace CWB.App.Services.BusinessProcesses
{
    public class BAService : IBAService
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;

        public BAService(ILoggerManager logger, IOptions<ApiUrls> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));
        }
        public async Task<WorkOrdersVM> PostWO(WorkOrdersVM workOrdersVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/workorder");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            workOrdersVMs.TenantId = tenantId;
            return await RestHelper<WorkOrdersVM>.PostAsync(uri, workOrdersVMs, headers);
        }

        public async Task<List<WorkOrdersVM>> MultiplePostWO(IEnumerable<WorkOrdersVM> workOrdersVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/multipleworkorder");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in workOrdersVMs)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<WorkOrdersVM>>.PostAsync(uri, workOrdersVMs, headers);
        }

        public async Task<List<WorkOrdersVM>> BOMTempPOst(IEnumerable<BOMTempVM> bOMTempVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/bomtemp");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in bOMTempVMs)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<WorkOrdersVM>>.PostAsync(uri, bOMTempVMs, headers);
        }

        public async Task<List<WOSOVM>> PostWoSoRel(IEnumerable<WOSOVM> wOSOVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/wosorel");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<WOSOVM>>.PostAsync(uri, wOSOVMs, headers);
        }

        public async Task<IEnumerable<WorkOrdersVM>> AllWorkOrders()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbpwo/allworkorders/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<WorkOrdersVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<CustomerOrderRowVM>> GetCustomerOrders()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/getcustomerorders/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<CustomerOrderRowVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<POLogVM>> GetPOLogs(long customerOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/getpologs/{tenantId}/{customerOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<POLogVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<SalesOrderVM>> AllSalesOrders()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/allsalesorders/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<SalesOrderVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<SalesOrderVM>> GetSalesOrders(long customerOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/getsalesorders/{tenantId}/{customerOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<SalesOrderVM>>.GetAsync(uri, headers);
        }

        public async Task<SalesOrderVM> GetOneSO(long salesOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/getsinglesalesorder/{tenantId}/{salesOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<SalesOrderVM>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<DeliveryScheduleVM>> GetSchedules(long customerOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/getschedules/{tenantId}/{customerOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DeliveryScheduleVM>>.GetAsync(uri, headers);
        }

        public async Task<SOAggregateVM> GetSOAggregate(long customerOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/getsoaggregate/{tenantId}/{customerOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<SOAggregateVM>.GetAsync(uri, headers);
        }

        public async Task<string> HelloWorld()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/helloworld/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<string>.GetAsync(uri, headers);
        }

        public async Task<CustomerOrderVM> PostCustomerOrder(CustomerOrderVM customerOrderVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/custorder");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            customerOrderVM.TenantId = tenantId;
            return await RestHelper<CustomerOrderVM>.PostAsync(uri, customerOrderVM, headers);
        }

        public async Task<DeliveryScheduleVM> PostDeliverySchedule(DeliveryScheduleVM deliveryScheduleVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/deliveryschedule");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            deliveryScheduleVM.TenantId = tenantId;
            return await RestHelper<DeliveryScheduleVM>.PostAsync(uri, deliveryScheduleVM, headers);

        }
        public async Task<POLogVM> PostSOLog(POLogVM poLogVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/solog");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            poLogVM.TenantId = tenantId;
            return await RestHelper<POLogVM>.PostAsync(uri, poLogVM, headers);

        }
        public async Task<POLogVM> PostPOLog(POLogVM poLogVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/polog");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            poLogVM.TenantId = tenantId;
            return await RestHelper<POLogVM>.PostAsync(uri, poLogVM, headers);

        }

        public async Task<SalesOrderVM> PostSalesOrder(SalesOrderVM salesOrderVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/salesorder");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            salesOrderVM.TenantId = tenantId;
            return await RestHelper<SalesOrderVM>.PostAsync(uri, salesOrderVM, headers);
        }

        public async Task<SOAggregateVM> PostSOAggregate(SOAggregateVM sOAggregateVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/soaggregate");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            sOAggregateVM.TenantId = tenantId;
            return await RestHelper<SOAggregateVM>.PostAsync(uri, sOAggregateVM, headers);
        }

        /*
         *  public const string RemoveSalesOrder = Base + "/removesalesorder/{tenantId}/{salesOrderId}";
           public const string RemoveCustomerOder = Base + "/removecustomerorder/{tenantId}/{customerOrderId}";
           public const string RemoveDeliverSchedule = Base + "/removedeliveryschedule/{tenantId}/{scheduleId}";
           public const string RemoveOrderStatus = Base + "/removeorderstatus/{tenantId}/{orderStatusId}";
         */

        public async Task<bool> RemoveCustomerOder(long customerOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/removecustomerorder/{tenantId}/{customerOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<bool> RemoveDeliverySchedule(long scheduleId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/removedeliveryschedule/{tenantId}/{scheduleId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<bool> RemoveSalesOrder(long salesOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/removesalesorder/{tenantId}/{salesOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

       
        public async Task<bool> AddSalesOrders(long customerOrderId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/addsalesorders/{tenantId}/{customerOrderId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<bool> CheckPartNo(long partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/checkpartno/{partId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<BAStatusVM> GetBAStatus(long Id)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbba/getbastatus/{Id}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<BAStatusVM>.GetAsync(uri, headers);
        }
    }
}
