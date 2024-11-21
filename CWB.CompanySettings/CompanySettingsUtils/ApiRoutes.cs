namespace CWB.CompanySettings.CompanySettingsUtils
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Aquisition
        {
            public const string GetPOLogs = Base + "/getpologs/{tenantId}/{customerOrderId}";
            public const string GetSalesOrders = Base + "/getsalesorders/{tenantId}/{customerOrderId}";
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

        }

        public static class DocType
        {
            public const string GetDocumentTypes = Base + "/document-types/{tenantId}";
            public const string PostDocumentType = Base + "/document-type";
            public const string GetDocumentType = Base + "/getdoctype/{docTypeId}";
            public const string DelDocumentType = Base + "/deldoctype/{docTypeId}";
        }

        public static class Plant
        {
            public const string GetPlants = Base + "/plants/{tenantId}";
            public const string GetCitys = Base + "/getcitys/{tenantId}";
            public const string GetCountrys = Base + "/getcountry/{tenantId}";
            public const string CheckCountrys = Base + "/checkcountry/{country}";
            public const string CheckCity = Base + "/checkcity/{city}";
            public const string PostPlant = Base + "/plant";
            public const string CheckPlant = Base + "/plant-exist";
            public const string GetPlant = Base + "/getplant/{plantId}";
            public const string DelPlant = Base + "/delplant/{plantId}";

            public const string PostPlantWD = Base + "/plantwd";
            public const string PostCity = Base + "/postcity";
            public const string PostCountry = Base + "/postcountry";
            public const string GetPlantWD = Base + "/getplantwd/{tenantId}/{plantId}";
            public const string Holidays = Base + "/holidays/{tenantId}/{plantId}";
            public const string PostHoliday = Base + "/holiday";
            public const string DelHoliday = Base + "/delholiday/{tenantId}/{holidayId}";
        }
        public static class Department
        {
            public const string GetDepartments = Base + "/departments/{Id}/{tenantId}";
            public const string GetDepartmentsWithPlants = Base + "/plant-departments";
            public const string PostDepartment = Base + "/department";
            public const string DelDepartment = Base + "/deldepartment/{departmentId}";
            public const string CheckDepartment = Base + "/department-exist";
        }

        public static class Designation
        {
            public const string GetDesignations = Base + "/designations/{tenantId}";
            public const string PostDesignation = Base + "/designation";
            public const string DelDesignation = Base + "/deldesignation/{designationId}";
        }
    }
}
