using System.Collections;
using System.Collections.Generic;

namespace CWB.App.Models.Routing
{
    public class RoutingListItemVM
    {
        public long ManufacturedPartId { get; set; }
        public string PartNo { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public string PartDescription { get; set; }
        public string Status { get; set; }
        public int RoutingId { get; set; } //with/without
        public string MasterPartType { get; set; }
        public bool HasRouting { get; set; }
        public int NoOfRoutes { get; set; } = 0;
        public string MandocAvl { get; set; } = string.Empty;

        public IEnumerable<RoutingVM> RoutingVMs { get; set; }
    }
}
