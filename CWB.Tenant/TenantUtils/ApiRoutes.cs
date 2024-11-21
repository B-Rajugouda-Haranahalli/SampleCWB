namespace CWB.Tenant.TenantUtils
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        public static class TenantRequest
        {
            public const string Request = Base + "/request";
            public const string GetRequest = Base + "/request/{Id}";
            public const string RequestAR = Base + "/approve-reject";
            public const string RequestByStatus = Base + "/request-by-status";
            public const string RequestARInternal = Base + "/approve-reject-int";
        }

        public static class Tenants
        {
            public const string Tenant = Base + "/tenant";
            public const string TenantInternal = Base + "/tenant-int";
            public const string GetTenant = Base + "/tenant/{Id}";
            public const string GetTenants = Base + "/tenant";
            public const string TenantStatus = Base + "/tenant-status";
        }
    }
}
