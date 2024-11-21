namespace CWB.CompanySettings.ViewModels.Location
{
   
    public class PlantVM
    {
        public long WDId { get; set; }
        public long PlantId { get; set; }
        public string Name { get; set; }
        public bool IsMainPlant { get; set; }
        public bool IsProductDesigned { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string GstNo { get; set; }
        public string PanNo { get; set; }
        public string WeeklyOff1 { get; set; }
        public int NoOfShifts { get; set; }
        public string FirstShiftStartTime { get; set; }
        public long TenantId { get; set; }
    }
}
