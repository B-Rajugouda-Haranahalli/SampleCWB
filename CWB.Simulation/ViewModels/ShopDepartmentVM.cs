namespace CWB.Simulation.ViewModels
{
    public class ShopDepartmentVM
    {
        public long ShopDepartmentId { get; set; }
        public string Name { get; set; }
        public int NoOfShifts { get; set; }
        public long TenantId { get; set; }
        public long PlantId { get; set; }
        public string Activity { get; set; }
    }
}
