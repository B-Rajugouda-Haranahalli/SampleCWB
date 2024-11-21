using CWB.CommonUtils.Common;

namespace CWB.Simulation.Domain
{
    public class DocumentType : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
        public bool IsUploadedByUser { get; set; }
        public long ShopDepartmentId { get; set; }
        public ShopDepartment ShopDepartment { get; set; }
    }
}