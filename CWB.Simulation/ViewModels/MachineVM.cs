namespace CWB.Simulation.ViewModels
{
    public class MachineVM
    {
        public long MachineId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public long MachineTypeId { get; set; }
        public MachineTypeVM MachineType { get; set; }
        public long PlantId { get; set; }
        public PlantVM Plant { get; set; }
        public long ShopDepartmentId { get; set; }
        public ShopDepartmentVM ShopDepartment { get; set; }
        public long TenantId { get; set; }

    }
}
