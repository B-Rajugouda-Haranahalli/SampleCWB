namespace CWB.Modules.ModulesUtils
{
    public class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class ModuleType
        {
            public const string ModuleTypes = Base + "/AllActiveModules";
        }
        public static class Module
        {
            public const string ModulesByTenant = Base + "/AllModulesByTenant";
            public const string GetModule = Base + "/GetModule";
            public const string AddModule = Base + "/AddModule";
            public const string UpdateModule = Base + "/UpdateModule";
            public const string EnableorDisableModule = Base + "/EnableorDisableModule";
        }
    }
}
