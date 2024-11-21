using CWB.CommonUtils.Common;
using System;

namespace CWB.CompanySettings.ViewModels.Location
{

    public class HolidayVM
    {
        public long HolidayId { get; set; }
        public long PlantId { get; set; }
        public string Name { get; set; }
        public DateTime? HolidayDate { get; set; }
        public long TenantId { get; set; }
    }
}
