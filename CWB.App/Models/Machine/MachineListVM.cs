namespace CWB.App.Models.Machine
{
    public class MachineListVM
    {
        public long MachineId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string SlNo { get; set; }
        public long PlantId { get; set; }
        public string Plant { get; set; }
        public long ShopId { get; set; }
        public string Shop { get; set; }
    }
}
