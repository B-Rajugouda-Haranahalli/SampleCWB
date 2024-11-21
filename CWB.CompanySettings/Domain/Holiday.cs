using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.CompanySettings.Domain
{
    public class Holiday : BaseEntity
    {
        
        public long PlantId {  get; set; }
        public string Name { get; set; }
        public DateTime? HolidayDate { get; set; }
        public long TenantId { get; set; }
    }
}
