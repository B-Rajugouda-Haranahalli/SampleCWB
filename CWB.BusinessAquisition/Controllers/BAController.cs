using CWB.BusinessAquisition.BusinessAquisitionUtils;
using CWB.BusinessAquisition.Services;
using CWB.BusinessAquisition.ViewModels;
using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAapi.Controllers
{
    [ApiController]
    [Authorize]
    public class BAController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IBAService _baService;

        public BAController(ILoggerManager logger,
            IBAService baService)
        {
            _logger = logger;
            _baService = baService;
        }


        [HttpGet]
        [Route(ApiRoutes.Aquisition.GetPOLogs)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<POLogVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetPOLogs(long tenantId,long customerOrderId)
        {
            var pologs = await _baService.GetPOLogs(tenantId, customerOrderId);
            return Ok(pologs);
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.GetSalesOrders)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<SalesOrderVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetSalesOrders(long tenantId,long customerOrderId)
        {
            var pologs = await _baService.GetSalesOrders(tenantId,customerOrderId);
            return Ok(pologs);
        }
        [HttpGet]
        [Route(ApiRoutes.Aquisition.AllSalesOrders)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<SalesOrderVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> AllSalesOrders(long tenantId)
        {
            var pologs = await _baService.AllSalesOrders(tenantId);
            return Ok(pologs);
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.GetSingleSalesOrder)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SalesOrderVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetSingleSalesOrder(long tenantId,long salesOrderId)
        {
            var pologs = await _baService.GetSingleSalesOrder(tenantId,salesOrderId);
            return Ok(pologs);
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.GetCustomerOrders)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<CustomerOrderVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetCustomerOrders(long tenantId)
        {
            var pologs = await _baService.GetCustomerOrders(tenantId);
            foreach(CustomerOrderVM custO in pologs)
            {
                custO.LineNo = GetLineNoForCustomerOrder(tenantId, custO.CustomerOrderId).Result.ToString();
            }
            return Ok(pologs);
        }

        private async Task<int> GetLineNoForCustomerOrder(long tenantId, long customerOrderId)
        {
            var salesOrders = await _baService.GetSalesOrders(tenantId, customerOrderId);
            List<long> partIds = new List<long>();
            foreach(SalesOrderVM so in salesOrders)
            {
                if(!partIds.Contains(so.PartId))
                {
                    partIds.Add(so.PartId);
                }
            }
            return partIds.Count;
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.GetSchedules)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<DeliveryScheduleVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetSchedules(long tenantId,long customerOrderId)
        {
            var pologs = await _baService.GetSchedules(tenantId, customerOrderId);
            return Ok(pologs);
        }


        [HttpGet]
        [Route(ApiRoutes.Aquisition.HelloWorld)]
        [Produces(AppContentTypes.ContentType, Type = typeof(string))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<string> HelloWorld(long tenantId)
        {
            var pologs = _baService.HelloWorld();
            return pologs;
        }

        /*
         *  public const string PostSalesOrder = Base + "/salesorder";
            public const string PostCustomerOrder = Base + "/custorder";
            public const string PostDeliverySchedule = Base + "/deliveryschedule";
            public const string PostOrderStatus = Base + "/orderstatus";
            public const string PostPOLog = Base + "/polog";
         */
        //
        [HttpPost]
        [Route(ApiRoutes.Aquisition.PostSalesOrder)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SalesOrderVM))]
        public async Task<SalesOrderVM> SalesOrder(SalesOrderVM salesOrderVm)
        {
            var pologs = await _baService.SalesOrder(salesOrderVm);
            return pologs;
        }

        [HttpPost]
        [Route(ApiRoutes.Aquisition.PostCustomerOrder)]
        [Produces(AppContentTypes.ContentType, Type = typeof(CustomerOrderVM))]
        public async Task<CustomerOrderVM> CustomerOrder(CustomerOrderVM customerOrderVM)
        {
            var pologs = await _baService.CustomerOrder(customerOrderVM);
            return pologs;
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.AddSalesOrders)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DeliveryScheduleVM))]
        public async Task<bool> AddSalesOrders(long tenantId,long customerOrderId)
        {
            var pologs = await _baService.AddSalesOrders(tenantId, customerOrderId);
            return pologs;
        }

        //[HttpPost]
        //[Route(ApiRoutes.Aquisition.UpdateWoInSo)]
        //[Produces(AppContentTypes.ContentType, Type = typeof(List<SalesOrderVM>))]
        //public async Task<List<SalesOrderVM>> UpdateWoInSo(List<SalesOrderVM> listSo)
        //{
        //    var so = await _baService.UpdateWoInSo(listSo);
        //    return so;
        //}


        [HttpPost]
        [Route(ApiRoutes.Aquisition.PostDeliverySchedule)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DeliveryScheduleVM))]
        public async Task<DeliveryScheduleVM> DeliverySchedule (DeliveryScheduleVM deliveryScheduleVM)
        {
            var pologs = await _baService.DeliverySchedule(deliveryScheduleVM);
            return pologs;
        }
        [HttpPost]
        [Route(ApiRoutes.Aquisition.PostOrderStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(OrderStatusVM))]
        public async Task<OrderStatusVM> OrderStatus(OrderStatusVM customerOrderStatusVM)
        {
            var pologs = await _baService.OrderStatus(customerOrderStatusVM);
            return pologs;
        }

        [HttpPost]
        [Route(ApiRoutes.Aquisition.PostPOLog)]
        [Produces(AppContentTypes.ContentType, Type = typeof(POLogVM))]
        public async Task<POLogVM> POLog(POLogVM pOLogVM)
        {
            var pologs = await _baService.POLog(pOLogVM);
            return pologs;
        }

        [HttpPost]
        [Route(ApiRoutes.Aquisition.PostSOLog)]
        [Produces(AppContentTypes.ContentType, Type = typeof(POLogVM))]
        public async Task<POLogVM> SOLog(POLogVM pOLogVM)
        {
            var pologs = await _baService.SOLog(pOLogVM);
            return pologs;
        }

        [HttpPost]
        [Route(ApiRoutes.Aquisition.PostSOAggregate)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SOAggregateVM))]
        public async Task<SOAggregateVM> SOAggregate(SOAggregateVM sOAggregateVM)
        {
            var aggregate = await _baService.SOAggregate(sOAggregateVM);
            return aggregate;
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.GetSOAggregate)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SOAggregateVM))]
        public async Task<SOAggregateVM> GetSOAggregate(long tenantId, long customerOrderId)
        {
            var aggregatevm = await _baService.GetSOAggregate(tenantId, customerOrderId);
            return aggregatevm;
        }

            /*
             * public const string RemoveSalesOrder = Base + "/removesalesorder/{tenantId}/{salesOrderId}";
                public const string RemoveCustomerOder = Base + "/removecustomerorder/{tenantId}/{customerOrderId}";
                public const string RemoveDeliverSchedule = Base + "/removedeliveryschedule/{tenantId}/{scheduleId}";
                public const string RemoveOrderStatus = Base + "/removeorderstatus/{tenantId}/{orderStatusId}";

             */


            [HttpGet]
        [Route(ApiRoutes.Aquisition.RemoveSalesOrder)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<bool> RemoveSalesOrder(long tenantId,long salesOrderId)
        {
            var pologs = await _baService.RemoveSalesOrder(tenantId, salesOrderId);
            return pologs;
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.RemoveCustomerOder)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<bool> RemoveCustomerOder(long tenantId, long customerOrderId)
        {
            var pologs = await _baService.RemoveCustomerOder(tenantId, customerOrderId);
            return pologs;
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.RemoveDeliverSchedule)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<bool> RemoveDeliverySchedule(long tenantId, long scheduleId)
        {
            var pologs = await _baService.RemoveDeliverySchedule(tenantId, scheduleId);
            return pologs;
        }
        
        [HttpGet]
        [Route(ApiRoutes.Aquisition.RemoveOrderStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<bool> RemoveOrderStatus(long tenantId, long orderStatudId)
        {
            var pologs = await _baService.RemoveOrderStatus(tenantId, orderStatudId);
            return pologs;
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.CheckPartNo)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckPartNo(long partId)
        {
            bool exists = false;
            exists = _baService.CheckPartNo(partId);
            return Ok(exists);
        }

        [HttpGet]
        [Route(ApiRoutes.Aquisition.GetBAStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(BAStatusVM))]
        public async Task<IActionResult> GetBAStatus(long Id)
        {
            var bastatus = await _baService.GetBAStatus(Id);
            return Ok(bastatus);
        }

    }
}
