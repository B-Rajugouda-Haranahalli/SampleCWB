namespace CWB.App.Models.Plants
{
    public class PlantWorkingDetailsVM
    {
        public long WDId { get; set; }
        public long PlantId { get; set; }
        public string WeeklyOff1 { get; set; }

        public int NoOfShifts { get; set; }
        public string WeeklyOff2 { get; set; }
        public string FirstShiftStartTime { get; set; }
        public string SecondShiftStartTime { get; set; }
        public string ThirdShiftStartTime { get; set; }
        public string FirstShiftDuration { get; set; }
        public string SecondShiftDuration { get; set; }
        public string ThirdShiftDuration { get; set; }
        public long TenantId { get; set; }
    }
}
