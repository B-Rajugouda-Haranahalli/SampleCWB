using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.CompanySettings.Domain
{
    /*
     * `Id` bigint NOT NULL AUTO_INCREMENT,
  `PlantId` bigint NOT NULL,
  `WeeklyOff1` varchar(255) NOT NULL default "",
  `WeeklyOff2` varchar(255) NOT NULL default "",
  `NoOfShifts` int default 1,
  `FirstShiftStartTime` varchar(255) NOT NULL default "",
  `SecondShiftStartTime` varchar(255) NOT NULL default "",
  `ThirdShiftStartTime` varchar(255) NOT NULL default "",
  `FirstShiftDuration` varchar(255) NOT NULL default "",
  `SecondShiftDuration` varchar(255) NOT NULL default "",
  `ThirdShiftDuration` varchar(255) NOT NULL default "",
     */
    public class PlantWorkingDetails : BaseEntity
    {
        public long PlantId { get; set; }
        public Plant Plant { get; set; }
        public string WeeklyOff1 { get; set; }
        public string WeeklyOff2 { get; set; }
        public int NoOfShifts { get; set; }
        public string FirstShiftStartTime { get; set; }
        public string SecondShiftStartTime { get; set; }
        public string ThirdShiftStartTime { get; set; }
        public string FirstShiftDuration { get; set; }
        public string SecondShiftDuration { get; set; }
        public string ThirdShiftDuration { get; set; }
        public long TenantId { get; set; }
    }
}
