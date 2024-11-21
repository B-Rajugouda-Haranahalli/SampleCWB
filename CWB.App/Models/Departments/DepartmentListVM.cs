namespace CWB.App.Models.Departments
{
    public class DepartmentListVM
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
