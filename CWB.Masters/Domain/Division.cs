using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain
{
    public class Division : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public string PlantName { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string GstNo { get; set; }
        public string PanNo { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        public long TenantId { get; set; }
    }
}
