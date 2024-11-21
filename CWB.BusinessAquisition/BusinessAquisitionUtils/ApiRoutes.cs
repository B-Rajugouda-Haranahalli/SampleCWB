namespace CWB.BusinessAquisition.BusinessAquisitionUtils
{
    public class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        public static class Aquisition
        {
            public const string GetPOLogs = Base + "/getpologs/{tenantId}/{customerOrderId}";
            public const string GetSalesOrders = Base + "/getsalesorders/{tenantId}/{customerOrderId}";
            public const string GetSingleSalesOrder = Base + "/getsinglesalesorder/{tenantId}/{salesOrderId}";
            public const string AllSalesOrders = Base + "/allsalesorders/{tenantId}";
            public const string GetCustomerOrders = Base + "/getcustomerorders/{tenantId}";
            public const string GetSchedules = Base + "/getschedules/{tenantId}/{customerOrderId}";
            public const string AddSalesOrders = Base + "/addsalesorders/{tenantId}/{customerOrderId}";
            public const string HelloWorld = Base + "/helloworld/{tenantId}";

            public const string GetSOAggregate = Base + "/getsoaggregate/{tenantId}/{customerOrderId}";
            public const string PostSOAggregate = Base + "/soaggregate";

            public const string PostSalesOrder = Base + "/salesorder";

            public const string PostDeliverySchedule = Base + "/deliveryschedule";
            public const string PostCustomerOrder = Base + "/custorder";
            public const string PostOrderStatus = Base + "/orderstatus";
            public const string PostPOLog = Base + "/polog";
            public const string PostSOLog = Base + "/solog";


            public const string RemoveSalesOrder = Base + "/removesalesorder/{tenantId}/{salesOrderId}";
            public const string RemoveCustomerOder = Base + "/removecustomerorder/{tenantId}/{customerOrderId}";
            public const string RemoveDeliverSchedule = Base + "/removedeliveryschedule/{tenantId}/{scheduleId}";
            public const string RemoveOrderStatus = Base + "/removeorderstatus/{tenantId}/{orderStatusId}";
            
            public const string CheckPartNo = Base + "/checkpartno/{partId}";
            public const string GetBAStatus = Base + "/getbastatus/{Id}";
        }
    }
}
