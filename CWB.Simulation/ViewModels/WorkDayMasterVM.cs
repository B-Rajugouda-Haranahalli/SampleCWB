using CWB.Simulation.SimulationUtils;
using System;

namespace CWB.Simulation.ViewModels
{
    public class WorkDayMasterVM
    {
        public int WorkDayMasterId { get; set; }
        public Days WeeklyOff { get; set; }
        public int NoOfShifts { get; set; }
        public TimeSpan FirstShiftStartTime { get; set; }
        public TimeSpan FirstShiftDuration { get; set; }
        public TimeSpan SecondShiftDuration { get; set; }
        public TimeSpan ThirdShiftDuration { get; set; }
        public long TenantId { get; set; }
    }
}
