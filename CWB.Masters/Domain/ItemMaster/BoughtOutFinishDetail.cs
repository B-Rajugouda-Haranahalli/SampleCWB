using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.Masters.Domain.ItemMaster
{
    /**
     * `BoughtOutFinishMadeType` int DEFAULT NULL,
   PartId bigint not null,
   `PartDescription` varchar(4000) DEFAULT NULL,
   SupplierPartNo varchar(200) not null, 
   AddtionalInfo varchar(400) null,
   `Status` int not NULL,
  `StatusChangeReason` varchar(300) DEFAULT NULL,
  `RevNo` varchar(255) DEFAULT NULL,
  `RevDate` datetime DEFAULT NULL,
  `TenantId` bigint NOT NULL,
     * */
    public class BoughtOutFinishDetail : BaseEntity
    {
        //DbPart-Start
        public long BoughtOutFinishMadeType { get; set; }
        public int PartId { get; set; }
        public long UOMId { get; set; }
        public string SupplierPartNo { get; set; }
        public string AdditionalInfo { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }
        public int TimetoDeliverReorderQnty { get; set; }
        //DbPart-End
        public long TenantId { get; set; }


    }
}
