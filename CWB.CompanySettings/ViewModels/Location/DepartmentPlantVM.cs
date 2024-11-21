using System.Collections.Generic;

namespace CWB.CompanySettings.ViewModels.Location
{
    public class DepartmentPlantVM
    {
        public long TenantId { get; set; }
        public List<long> DepartmentIds { get; set; }
    }
}
