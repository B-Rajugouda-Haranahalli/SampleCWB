namespace CWB.Simulation.SimulationUtils
{
    public class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        public static class WorkDayMaster
        {
            public const string WorkDayMasterByTenant = Base + "/AllWorkDayMasterByTenant";
            public const string AddWorkDayMaster = Base + "/AddWorkDayMaster";
            public const string UpdateWorkDayMaster = Base + "/UpdateWorkDayMaster";
        }

        public static class Plant
        {
            public const string PlantByTenant = Base + "/AllPlantByTenant";
            public const string AddPlant = Base + "/AddPlant";
            public const string UpdatePlant = Base + "/UpdatePlant";
        }

        public static class ShopDepartment
        {
            public const string ShopDepartmentByTenant = Base + "/AllShopDepartmentByTenant";
            public const string ShopDepartmentByPlant = Base + "/AllShopDepartmentByPlant";
            public const string AddShopDepartment = Base + "/AddShopDepartment";
            public const string UpdateShopDepartment = Base + "/UpdateShopDepartment";
        }


        public static class Section
        {
            public const string SectionsByTenant = Base + "/SectionsByTenant";
            public const string SectionsByDeparment = Base + "/SectionsByDepartment";
            public const string SectionsBySection = Base + "/SectionsByParentSection";
            public const string SectionsById = Base + "/Section";
            public const string AddSection = Base + "/AddSection";
            public const string UpdateSection = Base + "/UpdateSection";
        }

        public static class Machine
        {
            public const string MachineByTenant = Base + "/AllMachineByTenant";
            public const string AddMachine = Base + "/AddMachine";
            public const string UpdateMachine = Base + "/UpdateMachine";
        }

        public static class MachineType
        {
            public const string MachineTypeByTenant = Base + "/AllMachineTypeByTenant";
            public const string AddMachineType = Base + "/AddMachineType";
            public const string UpdateMachineType = Base + "/UpdateMachineType";
        }

        public static class Vendor
        {
            public const string VendorsByTenant = Base + "/GetVendorsByTenant";
            public const string VendorsByType = Base + "/GetVendorsByType";
            public const string AddVendor = Base + "/AddVendor";
            public const string UpdateVendor = Base + "/UpdateVendor";
        }

        public static class MRBom
        {
            public const string MRBomByTenant = Base + "/GetMRBomByTenant";
            public const string MRBomById = Base + "/GetMRBomById";
            public const string AddMRBom = Base + "/AddMRBom";
            public const string UpdateMRBom = Base + "/UpdateMRBom";
        }

        public static class MRBomGroup
        {
            public const string MRBomGroupByTenant = Base + "/GetMRBomGroupByTenant";
            public const string AddMRBomGroup = Base + "/AddMRBomGroup";
            public const string UpdateMRBomGroup = Base + "/UpdateMRBomGroup";
        }
        public static class ItemMaster
        {
            public const string ItemMasterByTenant = Base + "/GetItemMasterByTenant";
            public const string AddItemMaster = Base + "/AddItemMaster";
            public const string UpdateItemMaster = Base + "/UpdateItemMaster";
        }
    }
}
