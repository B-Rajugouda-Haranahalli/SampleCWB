using CWB.CommonUtils.Common;

namespace CWB.CompanySettings.Domain
{
    public class Section : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
        public long ParentSectionId { get; set; }
        public long ShopDepartmentId { get; set; }
        public ShopDepartment ShopDepartment { get; set; }
    }
}