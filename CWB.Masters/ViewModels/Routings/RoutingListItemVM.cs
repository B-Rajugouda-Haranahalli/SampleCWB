namespace CWB.Masters.ViewModels.Routings
{
    public class RoutingListItemVM
    {
        public long ManufacturedPartId {  get; set; }
        public string PartNo { get; set; }
        public string CompanyName {  get; set; }
        public int CompanyId {  get; set; }
        public string PartDescription {  get; set; }   
        public string Status {  get; set; } 
        public int RoutingId {  get; set; } //with/without
        public string MasterPartType { get; set; }
        public bool HasRouting { get; set; }
        public int NoOfRoutes { get; set; } = 0;

    }
}
