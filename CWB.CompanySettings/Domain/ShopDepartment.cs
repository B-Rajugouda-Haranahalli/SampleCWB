using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.CompanySettings.Domain
{
    public class ShopDepartment : BaseEntity
    {
        public string Name { get; set; }
        public int NoOfShifts { get; set; }
        public long TenantId { get; set; }
        public long PlantId { get; set; }
        public string Activity { get; set; }
        public bool ProdDept { get; set; }
        public string Section { get; set; }
        public Plant Plant { get; set; }
        public ICollection<Section> Sections { get; set; }
        //public ICollection<DocumentType> DocumentTypes { get; set; }
    }
}