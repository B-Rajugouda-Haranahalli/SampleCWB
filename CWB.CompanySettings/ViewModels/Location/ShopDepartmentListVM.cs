namespace CWB.CompanySettings.ViewModels.Location
{
    public class ShopDepartmentListVM
    {
        public string Name { get; set; }
        public long DepartmentId { get; set; }
        public int NoOfShifts { get; set; }
        public long PlantId { get; set; }
        public string PlantName { get; set; }
        public string Activity { get; set; }
        public bool ProdDept { get; set; }
    }
}
